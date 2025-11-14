using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using MvcMovie.Data;
using MvcMovie.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MediaOptions>(
    builder.Configuration.GetSection("MediaOptions"));

builder.Services.Configure<VideoDetail>(
    builder.Configuration.GetSection("VideoDetail"));

builder.Services.AddScoped<IMediaService, MediaService>();


// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddSessionStateTempDataProvider();


builder.Services.AddSession();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.UseSession();


app.MapControllers();
app.MapDefaultControllerRoute();
app.MapControllerRoute(
    name: "default",
    pattern: "",
    defaults: new
    {
        Controller = "Home",
        Action = "Index"
    }
);


app.Run();
