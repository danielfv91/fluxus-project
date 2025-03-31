using System;

namespace Fluxus.WebApi.Features.CashFlows.DailyCashFlow
{
    public class DailyCashFlowRequest
    {
        public DateOnly? DateFrom { get; set; }
        public DateOnly? DateTo { get; set; }
    }
}
