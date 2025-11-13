using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

public class CatalogueController : Controller
{
    private readonly IMediaService _mediaService;

    public CatalogueController(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    public IActionResult Index()
    {
        var files = _mediaService.GetAllMediaFiles();
        return View(files);
    }
}
