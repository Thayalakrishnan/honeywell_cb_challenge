using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.ViewComponents;

public class CatViewComponent : ViewComponent
{
    private readonly IMediaService _mediaService;

    public CatViewComponent(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    public IViewComponentResult Invoke()
    {
        var files = _mediaService.GetAllMediaFiles();
        return View(files);
    }
}