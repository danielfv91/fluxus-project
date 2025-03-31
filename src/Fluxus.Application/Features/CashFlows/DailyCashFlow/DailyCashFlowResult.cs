using System;

namespace Fluxus.Application.Features.CashFlows.DailyCashFlow
{
    public class DailyCashFlowResult
    {
        public DateOnly Date { get; set; }

        public decimal TotalCredits { get; set; }

        public decimal TotalDebits { get; set; }

        public decimal DailyBalance => TotalCredits - TotalDebits;

        public decimal AccumulatedBalance { get; set; }
    }
}
