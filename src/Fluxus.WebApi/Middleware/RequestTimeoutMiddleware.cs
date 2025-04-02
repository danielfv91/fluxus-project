using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Fluxus.WebApi.Middleware
{
    public class RequestTimeoutMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TimeSpan _timeout;

        public RequestTimeoutMiddleware(RequestDelegate next, TimeSpan timeout)
        {
            _next = next;
            _timeout = timeout;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(context.RequestAborted);
            cts.CancelAfter(_timeout);

            try
            {
                context.Items["RequestTimeoutToken"] = cts.Token;
                await _next(context);
            }
            catch (OperationCanceledException) when (cts.IsCancellationRequested)
            {
                context.Response.StatusCode = StatusCodes.Status408RequestTimeout;
                await context.Response.WriteAsync("A requisição excedeu o tempo limite permitido.");
            }
        }
    }

    public static class RequestTimeoutMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestTimeout(this IApplicationBuilder builder, TimeSpan timeout)
        {
            return builder.UseMiddleware<RequestTimeoutMiddleware>(timeout);
        }
    }
}
