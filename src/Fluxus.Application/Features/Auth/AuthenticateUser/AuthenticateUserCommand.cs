using MediatR;

namespace Fluxus.Application.Features.Auth.AuthenticateUser;

public class AuthenticateUserCommand : IRequest<AuthenticateUserResult>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
