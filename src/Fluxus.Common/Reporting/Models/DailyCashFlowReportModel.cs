namespace Fluxus.Common.Reporting.Models
{
    public class DailyCashFlowReportModel
    {
        public DateOnly Date { get; set; }
        public decimal TotalCredits { get; set; }
        public decimal TotalDebits { get; set; }
        public decimal DailyBalance => TotalCredits - TotalDebits;
        public decimal AccumulatedBalance { get; set; }
    }
}
