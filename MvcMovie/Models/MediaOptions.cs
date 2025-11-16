namespace MvcMovie.Models;

public class MediaOptions
{
    public string MediaFolder { get; set; } = "wwwroot/media";
    public int MaxUploadSizeMB { get; set; } = 200;
    public string[] AllowedExtensions { get; set; } = [".mp4"];
}