using System.Security.Claims;

namespace Fluxus.WebApi.Common
{
    public static class HttpContextExtensions
    {
        public static Guid GetUserId(this HttpContext context)
        {
            var sub = context.User.FindFirstValue(ClaimTypes.NameIdentifier)
                      ?? context.User.FindFirstValue("sub");

            return Guid.TryParse(sub, out var id)
                ? id
                : throw new UnauthorizedAccessException("ID do usuário não encontrado no token.");
        }

        public static string? GetUserEmail(this HttpContext context)
        {
            return context.User.FindFirstValue(ClaimTypes.Email);
        }

        public static string? GetUserName(this HttpContext context)
        {
            return context.User.FindFirstValue(ClaimTypes.Name);
        }

        public static string? GetUserRole(this HttpContext context)
        {
            return context.User.FindFirstValue(ClaimTypes.Role);
        }
    }
}
