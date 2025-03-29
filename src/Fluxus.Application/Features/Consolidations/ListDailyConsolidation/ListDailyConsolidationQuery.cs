using MediatR;
using System;

namespace Fluxus.Application.Features.Consolidations.ListDailyConsolidation
{
    public class ListDailyConsolidationQuery : IRequest<IEnumerable<DailyConsolidationResult>>
    {
        public Guid UserId { get; set; }

        public DateOnly? DateFrom { get; set; }

        public DateOnly? DateTo { get; set; }
    }
}
