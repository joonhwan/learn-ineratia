using System.Linq.Dynamic.Core;
using AspApp.Database;
using AspApp.Models;
using AspApp.Models.Dto;
using InertiaCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using User = AspApp.Database.User;

namespace AspApp.Controllers;

public class UsersController : Controller
{
    private readonly AppDbContext _appDbContext;
    private readonly ILogger<UsersController> _logger;

    public UsersController(
        AppDbContext appDbContext,
        ILogger<UsersController> logger
    )
    {
        _appDbContext = appDbContext;
        _logger = logger;
    }

    public async Task<Response> Index([FromQuery] PaginationParameters paginationParameters, string? search)
    {
        _logger.LogInformation("UsersController.Index : Search: {Search}, Users : {PageNumber}", search, paginationParameters.Page);
        IQueryable<User> query = _appDbContext.Users
                .AsNoTracking()
                .OrderBy(u => u.Id)
            ;
        if (search is not null)
        {
            query = query
                .Where(u => EF.Functions.Like(u.Name, $"%{search}%"));
        }
        var data = new PaginatedResponse<UserDto>
        {
            PageSize = paginationParameters.PageSize,
            PageNumber = paginationParameters.Page,
            TotalCount = await query.CountAsync(),
            Items = query
                .Skip(int.Max(paginationParameters.Page - 1, 0) * paginationParameters.PageSize)
                .Take(paginationParameters.PageSize)
                .Select(u => new UserDto(u.Id, u.Name, u.Email))
                .ToArray()
        };
        return Inertia.Render("Users", new
        {
            Data = data,
            Search = search
        });
    }
    //
    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}