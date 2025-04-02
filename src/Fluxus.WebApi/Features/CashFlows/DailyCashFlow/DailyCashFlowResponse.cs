using System;

namespace Fluxus.WebApi.Features.CashFlows.DailyCashFlow
{
    public class DailyCashFlowResponse
    {
        public DateOnly Date { get; set; }

        public decimal TotalCredits { get; set; }

        public decimal TotalDebits { get; set; }

        public decimal DailyBalance => TotalCredits - TotalDebits;

        public decimal AccumulatedBalance { get; set; }
    }
}
