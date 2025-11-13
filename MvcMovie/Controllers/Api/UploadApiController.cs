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
    [RequestSizeLimit(200 * 1024 * 1024)] // 200 MB limit for this API only
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadFiles(List<IFormFile> files)
    {
        if (files == null || files.Count == 0)
            return BadRequest(new { message = "No files received." });

        var (success, message) = await _mediaService.SaveFilesAsync(files);

        if (!success)
        {
            _logger.LogWarning("Upload failed: {Message}", message);
            return StatusCode(StatusCodes.Status415UnsupportedMediaType, new { message });
        }

        return Ok(new { message });
    }
}
