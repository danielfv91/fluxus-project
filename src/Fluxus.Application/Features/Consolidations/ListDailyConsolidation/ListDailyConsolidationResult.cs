using System;

namespace Fluxus.Application.Features.Consolidations.ListDailyConsolidation
{
    public class ListDailyConsolidationResult
    {
        public DateOnly Date { get; set; }

        public decimal TotalCredits { get; set; }

        public decimal TotalDebits { get; set; }

        public decimal Balance => TotalCredits - TotalDebits;
    }
}
