using InertiaCore;
using Microsoft.AspNetCore.Mvc;

namespace AspApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public Task<Response> Index()
    {
        var data = new
        {
            Title = "ZibPage",
            Time = DateTime.Now.ToString("O"),
            Frameworks = new[]
            {
                "React",
                "Angular",
                "Vue",
                "Svelte",
                "Inertia",
                "Blazor",
                "Mantine",
            }
        };
        
        return Task.FromResult(
            Inertia.Render("Home", data)
        );
    }
    //
    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}