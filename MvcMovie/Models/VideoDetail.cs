namespace MvcMovie.Models;

public class VideoDetail
{
    public string FileName { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string Url { get; set; } = string.Empty;
}