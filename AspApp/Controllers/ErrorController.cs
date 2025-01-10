using System.Diagnostics;
using AspApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspApp.Controllers;

public class ErrorController : Controller
{
    [HttpGet("Error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("~/Views/Error.cshtml", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    
    // [Route("Error/{statusCode}")]
    // public IActionResult HttpStatusCodeHandler(int statusCode)
    // {
    //     switch (statusCode)
    //     {
    //         case 404:
    //             ViewBag.ErrorMessage = "페이지를 찾을 수 없습니다";
    //             break;
    //         case 500:
    //             ViewBag.ErrorMessage = "서버 오류가 발생했습니다";
    //             break;
    //     }
    //     return View("~/Views/Error.cshtml");
    // }
}