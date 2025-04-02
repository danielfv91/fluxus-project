using MediatR;

namespace Fluxus.Application.Features.Consolidations.ListDailyConsolidation
{
    public class ListDailyConsolidationQuery : IRequest<List<ListDailyConsolidationResult>>
    {
        public Guid UserId { get; set; }
        public DateOnly? DateFrom { get; set; }
        public DateOnly? DateTo { get; set; }
    }
}
