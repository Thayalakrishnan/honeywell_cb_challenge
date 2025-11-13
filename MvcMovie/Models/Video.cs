namespace MvcMovie.Models;

public class Video
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public long FileSize { get; set; } // in bytes
    public string Url { get; set; } = string.Empty;
}