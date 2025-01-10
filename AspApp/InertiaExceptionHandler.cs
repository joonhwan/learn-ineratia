// using InertiaCore;
// using Microsoft.AspNetCore.Diagnostics;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Abstractions;
//
// namespace AspApp;
//
// // 상태 코드를 enum으로 정의
// public enum HttpStatusCodes
// {
//     InternalServerError = 500,
//     ServiceUnavailable = 503,
//     NotFound = 404,
//     Forbidden = 403,
//     PageExpired = 419
// }
//
// // 
// public class InertiaExceptionHandler : IExceptionHandler
// {
//     private readonly IWebHostEnvironment _environment;
//     
//     // 상태 코드 배열을 상수로 정의
//     private static readonly int[] ErrorStatusCodes = new[] 
//     {
//         (int)HttpStatusCodes.InternalServerError,
//         (int)HttpStatusCodes.ServiceUnavailable,
//         (int)HttpStatusCodes.NotFound,
//         (int)HttpStatusCodes.Forbidden
//     };
//
//
//     
//     public InertiaExceptionHandler(IWebHostEnvironment environment)
//     {
//         _environment = environment;
//     }
//
//     public async ValueTask<bool> TryHandleAsync(
//         HttpContext httpContext,
//         Exception exception,
//         CancellationToken cancellationToken)
//     {
//         var response = httpContext.Response;
//         var request = httpContext.Request;
//
//         // 프로덕션 환경에서 특정 상태 코드 처리
//         if (!_environment.IsDevelopment() && ErrorStatusCodes.Contains(response.StatusCode))
//         {
//             var actionResult = Inertia.Render("Error", new { status = response.StatusCode });
//             await actionResult.ExecuteResultAsync(new ActionContext(
//                 httpContext,
//                 httpContext.GetRouteData(),
//                 new ActionDescriptor()
//             ));
//             
//             return true;
//         }
//         
//         // CSRF 토큰 만료 처리 (419 상태 코드)
//         if (response.StatusCode == 419)
//         {
//             response.Redirect(request.Headers["Referer"].ToString());
//             httpContext.Session.SetString("message", "The page expired, please try again.");
//             return true;
//         }
//
//         return false; // 여기서 처리되지 않은 것은 다른 예외 처리 로직을 사용하여 처리
//     }
// }