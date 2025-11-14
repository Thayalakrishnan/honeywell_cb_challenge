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
    
    public IActionResult Index(bool ShowUploadView = false)
    {
        var vm = new HomeViewModel
        {
            ShowUploadView = ShowUploadView
        };
        return View(vm);
    }
    
    public IActionResult ShowUpload()
    {
        var currentState = true;
        return RedirectToAction("Index", new { ShowUploadView = !currentState });
    }

    public IActionResult ShowCatalogue()
    {
        var currentState = false;
        return RedirectToAction("Index", new { ShowUploadView = !currentState });
    }
}
