using System.Threading;
using System.Threading.Tasks;

namespace Fluxus.Application.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
