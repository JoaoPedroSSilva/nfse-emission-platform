
using Microsoft.Extensions.Options;

namespace Nfse.Gateway.Security
{
    public class ApiKeyMiddleware : IMiddleware
    {
        private readonly GatewayOptions _options;

        public ApiKeyMiddleware(IOptions<GatewayOptions> options) => _options = options.Value;
        
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (!context.Request.Path.StartsWithSegments("/api"))
            {
                await next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue("X-Api-Key", out var provided) ||
                string.IsNullOrWhiteSpace(provided) ||
                !string.Equals(provided!, _options.ApiKey, StringComparison.Ordinal))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Missing or invalid API key.");
                return;
            }

            await next(context);
        }
    }
}
