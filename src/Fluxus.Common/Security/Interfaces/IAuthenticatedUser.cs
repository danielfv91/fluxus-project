namespace Fluxus.Common.Security.Interfaces
{
    public interface IAuthenticatedUser
    {
        Guid Id { get; }
        string Email { get; }
    }
}
