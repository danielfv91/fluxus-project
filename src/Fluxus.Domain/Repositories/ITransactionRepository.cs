using Fluxus.Domain.Entities;

namespace Fluxus.Application.Domain.Repositories
{
    public interface ITransactionRepository
    {
        Task AddAsync(Transaction transaction, CancellationToken cancellationToken);
    }
}
