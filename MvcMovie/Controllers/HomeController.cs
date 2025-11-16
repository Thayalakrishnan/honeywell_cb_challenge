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
    
    private const string TempKey = "ShowUploadView";


    public IActionResult Index()
    {
        bool ShowUploadView = TempData.ContainsKey(TempKey) ? Convert.ToBoolean(TempData[TempKey]) : false;
        TempData.Keep(TempKey);
        
        var vm = new HomeViewModel
        {
            ShowUploadView = ShowUploadView
        };
        return View(vm);
    }
    
    public IActionResult ShowUpload()
    {
        TempData[TempKey] = true;
        return RedirectToAction("Index");
    }

    public IActionResult ShowCatalogue()
    {
        TempData[TempKey] = false;
        return RedirectToAction("Index");
    }
}
