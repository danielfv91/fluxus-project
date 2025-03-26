using FluentValidation;

namespace Fluxus.WebApi.Features.Auth.AuthenticateUserFeature
{
    /// <summary>
    /// Validador para AuthenticateUserRequest.
    /// </summary>
    public class AuthenticateUserRequestValidator : AbstractValidator<AuthenticateUserRequest>
    {
        public AuthenticateUserRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-mail é obrigatório.")
                .EmailAddress().WithMessage("Formato de e-mail inválido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Senha é obrigatória.");
        }
    }
}
