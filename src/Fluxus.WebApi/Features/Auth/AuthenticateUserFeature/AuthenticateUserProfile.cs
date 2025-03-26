using AutoMapper;
using Fluxus.Application.Features.Auth.AuthenticateUser;

namespace Fluxus.WebApi.Features.Auth.AuthenticateUserFeature
{
    /// <summary>
    /// Perfil do AutoMapper para autenticação de usuário.
    /// </summary>
    public sealed class AuthenticateUserProfile : Profile
    {
        public AuthenticateUserProfile()
        {
            CreateMap<AuthenticateUserRequest, AuthenticateUserCommand>();
            CreateMap<AuthenticateUserResult, AuthenticateUserResponse>();
        }
    }
}
