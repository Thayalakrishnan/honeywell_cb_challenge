using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

public class UpViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}