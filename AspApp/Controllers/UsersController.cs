using InertiaCore;
using Microsoft.AspNetCore.Mvc;

namespace AspApp.Controllers;

public class UsersController : Controller
{
    private readonly ILogger<UsersController> _logger;

    public UsersController(ILogger<UsersController> logger)
    {
        _logger = logger;
    }

    public async Task<Response> Index()
    {
        var data = new
        {
        };
        await Task.Delay(TimeSpan.FromSeconds(0));
        return Inertia.Render("Users", data);
    }
    //
    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}