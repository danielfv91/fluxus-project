using Fluxus.Application.Domain.Repositories;
using Fluxus.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fluxus.ORM.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DbContext _context;

        public TransactionRepository(DbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Transaction transaction, CancellationToken cancellationToken)
        {
            await _context.Set<Transaction>().AddAsync(transaction, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
