using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

public class HomeController : Controller
{
    private const string TempKey = "ShowUpload";
    private readonly IMediaService _mediaService;
    
    [TempData]
    public bool ShowUploadView { get; set; } = false;

    public HomeController(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }


    public IActionResult Index()
    {

        //if (TempData.ContainsKey(TempKey))
        //{
        //    // Convert stored temp value to bool
        //    showUpload = (bool)TempData[TempKey];
        //}

        //var vm = new HomeViewModel
        //{
        //    ShowUploadView = ShowUploadView
        //};

        //return ViewComponent("Cat", new { });
        return View();
    }
    
    public IActionResult ShowUpload()
    {
        TempData[TempKey] = true;
        ShowUploadView = true;
        return RedirectToAction("Index");
    }

    public IActionResult ShowCatalogue()
    {
        TempData[TempKey] = false;
        ShowUploadView = false;
        return RedirectToAction("Index");
    }
    
    public IActionResult Catalogue()
    {
        var files = _mediaService.GetAllMediaFiles();
        return View(files);
    }
}
