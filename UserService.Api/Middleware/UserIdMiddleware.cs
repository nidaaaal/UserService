using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace UserService.Api.Middleware
{
    public class UserIdMiddleware
    {
        private readonly RequestDelegate _next;

        public UserIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.User.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if(userIdClaim!=null && Guid.TryParse(userIdClaim.Value,out Guid userId)){
                    Console.Write(userId);
                    httpContext.Items["userId"] = userId;
                }
            }
           await _next(httpContext);
        }
    }

    public static class UserIdMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserIdMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserIdMiddleware>();
        }
    }
}
