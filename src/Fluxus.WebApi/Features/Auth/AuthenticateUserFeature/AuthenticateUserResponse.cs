namespace Fluxus.WebApi.Features.Auth.AuthenticateUserFeature
{
    /// <summary>
    /// Representa a resposta retornada após autenticação de usuário.
    /// </summary>
    public sealed class AuthenticateUserResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
