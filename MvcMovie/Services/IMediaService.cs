using MvcMovie.Models;

public interface IMediaService
{
    Task<(bool Success, string Message)> SaveFilesAsync(IEnumerable<IFormFile> files);
    IEnumerable<VideoDetail> GetAllMediaFiles();
}
