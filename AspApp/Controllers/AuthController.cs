using Microsoft.AspNetCore.Mvc;

namespace AspApp.Controllers;

public class AuthController : Controller
{
    // Post
    [HttpPost]
    [Route("/logout")]
    public async Task<IActionResult> Logout()
    {
        var request = HttpContext.Request;
        // read json data from request body
        var data = await request.ReadFromJsonAsync<Dictionary<string, string>>();
        
        return Ok(new
        {
            data = data,
        });
    }
}