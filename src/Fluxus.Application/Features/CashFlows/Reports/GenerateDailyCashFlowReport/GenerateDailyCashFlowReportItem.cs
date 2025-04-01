namespace Fluxus.Application.Features.CashFlows.DailyCashFlow.Reports.GenerateDailyCashFlowReport
{
    public class GenerateDailyCashFlowReportItem
    {
        public DateOnly Date { get; set; }
        public decimal TotalCredits { get; set; }
        public decimal TotalDebits { get; set; }
        public decimal DailyBalance => TotalCredits - TotalDebits;
        public decimal AccumulatedBalance { get; set; }
    }
}
