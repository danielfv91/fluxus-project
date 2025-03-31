using Fluxus.Domain.Entities;

namespace Fluxus.Application.Domain.Repositories
{
    public interface IDailyConsolidationRepository
    {
        Task<DailyConsolidation?> GetByUserAndDateAsync(Guid userId, DateOnly date, CancellationToken cancellationToken);
        Task AddAsync(DailyConsolidation consolidation, CancellationToken cancellationToken);
        Task UpdateAsync(DailyConsolidation consolidation, CancellationToken cancellationToken);
        Task<IEnumerable<DailyConsolidation>> GetAllByUserAsync(Guid userId, CancellationToken cancellationToken);

    }
}
