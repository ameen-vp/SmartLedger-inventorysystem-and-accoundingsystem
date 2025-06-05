using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Applications.Middleware
{
    public class Usermiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<Usermiddleware> _logger;

        public Usermiddleware(RequestDelegate next, ILogger<Usermiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
                {
                    context.Items["UserId"] = userId;
                    _logger.LogInformation("UserMiddleware executed. UserId set to {UserId}", userId);
                }
                else
                {
                    _logger.LogWarning("UserId claim missing or invalid.");
                }
            }
            else
            {
                _logger.LogWarning("User not authenticated.");
            }

            await _next(context);
        }
    }
}
