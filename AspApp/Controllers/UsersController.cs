using AspApp.Services;
using InertiaCore;
using Microsoft.AspNetCore.Mvc;

namespace AspApp.Controllers;

public class UsersController : Controller
{
    private readonly IJsonPlaceholderService _jsonPlaceholderService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(
        IJsonPlaceholderService jsonPlaceholderService,
        ILogger<UsersController> logger
    )
    {
        _jsonPlaceholderService = jsonPlaceholderService;
        _logger = logger;
    }

    public async Task<Response> Index([FromQuery]PaginationParameters paginationParameters, string? search)
    {
        var users = await _jsonPlaceholderService.QueryUsers(PaginationParameters.All);
        var data = users?
            .Items
            .Where(u => search is null || (u.Name != null && u.Name.Contains(search, StringComparison.CurrentCultureIgnoreCase)))
            .Skip(int.Max(paginationParameters.PageNumber - 1, 0) * paginationParameters.PageSize)
            .Take(paginationParameters.PageSize)
            .Select(u => new { u.Id, u.Name, u.Email })
            .ToArray();
        await Task.Delay(TimeSpan.FromSeconds(0));
        _logger.LogInformation("UsersController.Index : Search: {Search}, Users : {Users}", search, data?.Length);
        return Inertia.Render("Users", new
        {
            Users = data,
            Search = search,
        });
    }
    //
    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}