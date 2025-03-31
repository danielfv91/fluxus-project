using Fluxus.Application.Domain.Repositories;
using Fluxus.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fluxus.ORM.Repositories
{
    public class DailyConsolidationRepository : IDailyConsolidationRepository
    {
        private readonly DefaultContext _context;

        public DailyConsolidationRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<DailyConsolidation?> GetByUserAndDateAsync(Guid userId, DateOnly date, CancellationToken cancellationToken)
        {
            return await _context.DailyConsolidations
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == userId && c.Date == date, cancellationToken);
        }

        public async Task AddAsync(DailyConsolidation consolidation, CancellationToken cancellationToken)
        {
            await _context.DailyConsolidations.AddAsync(consolidation, cancellationToken);
        }

        public Task UpdateAsync(DailyConsolidation consolidation, CancellationToken cancellationToken)
        {
            _context.DailyConsolidations.Update(consolidation);
            return Task.CompletedTask;
        }
        public async Task<IEnumerable<DailyConsolidation>> GetAllByUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.DailyConsolidations
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .ToListAsync(cancellationToken);
        }

    }
}
