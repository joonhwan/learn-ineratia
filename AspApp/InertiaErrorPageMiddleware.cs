using InertiaCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace AspApp;


// 상태 코드를 enum으로 정의
public enum HttpStatusCodes
{
    InternalServerError = 500,
    ServiceUnavailable = 503,
    NotFound = 404,
    Forbidden = 403,
    PageExpired = 419
}


public class InertiaErrorPageMiddleware
{
     private static readonly int[] ErrorStatusCodes =
     [
         (int)HttpStatusCodes.InternalServerError,
         (int)HttpStatusCodes.ServiceUnavailable,
         (int)HttpStatusCodes.NotFound,
         (int)HttpStatusCodes.Forbidden
     ];
     
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _environment;
    
    public InertiaErrorPageMiddleware(RequestDelegate next, IWebHostEnvironment environment)
    {
        _next = next;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
        await HandleExceptionAsync(context);
        
    }

    private async Task<bool> HandleExceptionAsync(HttpContext httpContext)
    {
        var response = httpContext.Response;
        var request = httpContext.Request;

        // 프로덕션 환경에서 특정 상태 코드 처리
        var isDevelopment = false; //_environment.IsDevelopment();
        if (!isDevelopment && ErrorStatusCodes.Contains(response.StatusCode))
        {
            var actionResult = Inertia.Render("Error", new { status = response.StatusCode });
            await actionResult.ExecuteResultAsync(new ActionContext(
                httpContext,
                httpContext.GetRouteData(),
                new ActionDescriptor()
            ));
        }
        
        // CSRF 토큰 만료 처리 (419 상태 코드)
        if (response.StatusCode == 419)
        {
            response.Redirect(request.Headers["Referer"].ToString());
            httpContext.Session.SetString("message", "The page expired, please try again.");
            return true;
        }

        return false; // 여기서 처리되지 않은 것은 다른 예외 처리 로직을 사용하여 처리
    }
}