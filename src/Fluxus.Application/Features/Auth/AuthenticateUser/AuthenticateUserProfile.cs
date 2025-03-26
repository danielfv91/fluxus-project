using AutoMapper;
using Fluxus.Domain.Entities;

namespace Fluxus.Application.Features.Auth.AuthenticateUser;

public sealed class AuthenticateUserProfile : Profile
{
    public AuthenticateUserProfile()
    {
        CreateMap<User, AuthenticateUserResult>();
    }
}
