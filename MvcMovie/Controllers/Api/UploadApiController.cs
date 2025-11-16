using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

[ApiController]
[Route("api/upload")]
public class UploadApiController : ControllerBase
{
    private readonly IMediaService _mediaService;
    private readonly ILogger<UploadApiController> _logger;

    public UploadApiController(IMediaService mediaService, ILogger<UploadApiController> logger)
    {
        _mediaService = mediaService;
        _logger = logger;
    }

    [HttpPost]
    [RequestSizeLimit(200 * 1024 * 1024)]
    [RequestFormLimits(MultipartBodyLengthLimit = 200 * 1024 * 1024)]
    public async Task<IActionResult> UploadFiles(List<IFormFile> files)
    {
        if (files == null || files.Count == 0)
        {
            return BadRequest(new { message = "No files received." });
        }

        var (success, message, status) = await _mediaService.SaveFilesAsync(files);

        if (!success)
        {
            _logger.LogWarning("Upload failed: {Message}", message);
            switch (status)
            {
                case 200: // OK
                    return StatusCode(StatusCodes.Status200OK, new { message });
                case 201: // Created
                    return StatusCode(StatusCodes.Status201Created, new { message });
                case 409: // Conflict
                    return StatusCode(StatusCodes.Status409Conflict, new { message });
                case 415:
                    return StatusCode(StatusCodes.Status415UnsupportedMediaType, new { message });
                case 413: // Payload Too Large
                    return StatusCode(StatusCodes.Status413PayloadTooLarge, new { message });
                case 500: // Internal Server Error
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message });
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message });
            }
        }

        return Ok(new { message });
    }
}
