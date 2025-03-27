namespace Fluxus.Common.Security.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(IUser user);
    }
}
