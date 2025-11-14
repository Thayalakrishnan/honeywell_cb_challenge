using Microsoft.Extensions.Options;

using MvcMovie.Models;
using MvcMovie.Services.Exceptions;

public class MediaService : IMediaService
{
    private readonly MediaOptions _options;
    private readonly ILogger<MediaService> _logger;
    private readonly IWebHostEnvironment _env;

    public MediaService(
        IOptions<MediaOptions> options,
        ILogger<MediaService> logger,
        IWebHostEnvironment env)
    {
        _options = options.Value;
        _logger = logger;
        _env = env;
    }

    public async Task<(bool Success, string Message)> SaveFilesAsync(IEnumerable<IFormFile> files)
    {
        var mediaPath = Path.Combine(_env.WebRootPath, "media");

        Directory.CreateDirectory(mediaPath); // ensure folder exists

        foreach (var file in files)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_options.AllowedExtensions.Contains(extension))
                return (false, $"File '{file.FileName}' has invalid extension.");

            if (file.Length > _options.MaxUploadSizeMB * 1024 * 1024)
                return (false, $"File '{file.FileName}' exceeds max size.");

            var safeFileName = Path.GetFileName(file.FileName); // prevents path traversal
            var filePath = Path.Combine(mediaPath, safeFileName);
            
            _logger.LogInformation("safeFileName: {safeFileName}", safeFileName);
            _logger.LogInformation("filePath: {filePath}", filePath);


            foreach (var existingfile in Directory.GetFiles(mediaPath))
            {
                var info = new FileInfo(existingfile);
                if (info.Name == safeFileName)
                {
                    _logger.LogInformation("SAME FILE NAME {safeFileName}=={info.Name}", safeFileName, info.Name);
                    //return (false, $"File '{file.FileName}' already exists in catalogue.");
                    throw new ResourceConflictException($"A video file named '{safeFileName}' already exists.");
                }
            }

            try
            {
                await using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                await file.CopyToAsync(stream);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving file {FileName}", file.FileName);
                return (false, $"Error saving '{file.FileName}'.");
            }
        }

        return (true, "All files uploaded successfully.");
    }

    public IEnumerable<VideoDetail> GetAllMediaFiles()
    {
        var mediaPath = Path.Combine(_env.WebRootPath, "media");
        if (!Directory.Exists(mediaPath))
            yield break;

        foreach (var file in Directory.GetFiles(mediaPath))
        {
            var info = new FileInfo(file);
            yield return new VideoDetail
            {
                FileName = info.Name,
                FileSize = info.Length,
                Url = $"/media/{info.Name}"
            };
        }
    }
}
