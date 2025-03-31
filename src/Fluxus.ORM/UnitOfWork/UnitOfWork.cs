using Fluxus.Application.Domain.UnitOfWork;
using Fluxus.ORM;
using Microsoft.EntityFrameworkCore;

namespace Fluxus.ORM.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DefaultContext _context;

        public UnitOfWork(DefaultContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
