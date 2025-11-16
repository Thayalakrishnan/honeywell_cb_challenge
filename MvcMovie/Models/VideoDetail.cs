namespace MvcMovie.Models;

public class VideoDetail
{
    public string FileName { get; set; } = string.Empty;
    public long FileSize { get; set; } // in bytes
    public string Url { get; set; } = string.Empty;
}