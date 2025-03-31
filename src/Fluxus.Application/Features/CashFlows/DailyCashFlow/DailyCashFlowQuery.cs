using MediatR;
using System;
using System.Collections.Generic;

namespace Fluxus.Application.Features.CashFlows.DailyCashFlow
{
    public class DailyCashFlowQuery : IRequest<List<DailyCashFlowResult>>
    {
        public Guid UserId { get; set; }
        public DateOnly? DateFrom { get; set; }
        public DateOnly? DateTo { get; set; }
    }
}
