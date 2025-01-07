using InertiaCore;
using Microsoft.AspNetCore.Mvc;

namespace AspApp.Controllers;

public class SettingsController : Controller
{
    private readonly ILogger<SettingsController> _logger;

    public SettingsController(ILogger<SettingsController> logger)
    {
        _logger = logger;
    }
    
    // GET
    public Task<Response> Index()
    {
        var data = new
        {
        };
        return Task.FromResult(
            Inertia.Render("Settings", data)
        );
    }
}