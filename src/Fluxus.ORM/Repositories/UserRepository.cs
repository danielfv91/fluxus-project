using Fluxus.Application.Domain.Repositories;
using Fluxus.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fluxus.ORM.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DefaultContext _context;

        public UserRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }
    }
}
