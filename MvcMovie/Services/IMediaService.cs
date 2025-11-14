using MvcMovie.Models;

public interface IMediaService
{
    Task<(bool Success, string Message, int Status)> SaveFilesAsync(IEnumerable<IFormFile> files);
    IEnumerable<VideoDetail> GetAllMediaFiles();
}
