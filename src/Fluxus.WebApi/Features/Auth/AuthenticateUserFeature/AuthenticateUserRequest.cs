namespace Fluxus.WebApi.Features.Auth.AuthenticateUserFeature
{
    /// <summary>
    /// Representa a requisição de autenticação de usuário.
    /// </summary>
    public class AuthenticateUserRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
