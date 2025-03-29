namespace Fluxus.WebApi.Features.Consolidations.ListDailyConsolidation
{
    public class ListDailyConsolidationResponse
    {
        public DateOnly Date { get; set; }

        public decimal TotalCredits { get; set; }

        public decimal TotalDebits { get; set; }

        public decimal Balance => TotalCredits - TotalDebits;
    }
}
