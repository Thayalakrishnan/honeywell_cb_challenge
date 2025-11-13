using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers;

public class HelloWorldController : Controller
{
    // 
    // GET: /HelloWorld/
    // default
    public IActionResult Index()
    {
        return View();
    }
    // 
    // GET: /HelloWorld/Welcome/ 
    public IActionResult Welcome(string name, int numTimes = 1)
    {
        ViewData["Message"] = "Hello " + name;
        ViewData["NumTimes"] = numTimes;
        return View();
    }
    // 
    // Maps both of these, given the URL structure 
    // configured in Program.cs
    // GET: /HelloWorld/WelcomeID?name=Rick&id=3
    // GET: /HelloWorld/WelcomeID/3?name=Rick
    public string WelcomeID(string name, int ID = 1)
    {
        return HtmlEncoder.Default.Encode($"Hello {name}, ID is: {ID}");
    }
}