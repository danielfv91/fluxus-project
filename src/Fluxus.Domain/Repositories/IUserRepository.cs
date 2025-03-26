using Fluxus.Domain.Entities;

namespace Fluxus.Application.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
