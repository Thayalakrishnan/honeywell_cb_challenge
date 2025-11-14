using Microsoft.Extensions.Options;

using MvcMovie.Models;

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

    public async Task<(bool Success, string Message, int Status)> SaveFilesAsync(IEnumerable<IFormFile> files)
    {
        var mediaPath = Path.Combine(_env.WebRootPath, "media");

        Directory.CreateDirectory(mediaPath);

        foreach (var file in files)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_options.AllowedExtensions.Contains(extension))
                return (false, $"File '{file.FileName}' has invalid extension.", 415);

            if (file.Length > _options.MaxUploadSizeMB * 1024 * 1024)
                return (false, $"File '{file.FileName}' exceeds max size.", 413);

            var safeFileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(mediaPath, safeFileName);
            
            foreach (var existingfile in Directory.GetFiles(mediaPath))
            {
                var info = new FileInfo(existingfile);
                if (info.Name == safeFileName)
                {
                    _logger.LogInformation("SAME FILE NAME {safeFileName}=={info.Name}", safeFileName, info.Name);
                    return (false, $"File '{safeFileName}' already exists.", 409);
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
                return (false, $"Error saving '{file.FileName}'.", 500);
            }
        }

        return (true, "All files uploaded successfully.", 201);
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
