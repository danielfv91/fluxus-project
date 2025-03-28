using Fluxus.Application.Domain.Repositories;
using Fluxus.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fluxus.ORM.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DefaultContext _context;

        public TransactionRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Transaction transaction, CancellationToken cancellationToken)
        {
            await _context.Set<Transaction>().AddAsync(transaction, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Transaction>> GetByPeriodAsync(
            Guid userId,
            DateTime? dateFrom,
            DateTime? dateTo,
            CancellationToken cancellationToken)
        {
            var query = _context.Transactions
                .AsNoTracking()
                .Where(t => t.UserId == userId);

            if (dateFrom.HasValue)
                query = query.Where(t => t.Date >= dateFrom.Value);

            if (dateTo.HasValue)
                query = query.Where(t => t.Date <= dateTo.Value);

            return await query
                .OrderByDescending(t => t.Date)
                .ToListAsync(cancellationToken);
        }
    }
}
