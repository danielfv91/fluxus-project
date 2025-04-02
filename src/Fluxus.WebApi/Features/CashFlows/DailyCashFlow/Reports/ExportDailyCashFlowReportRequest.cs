namespace Fluxus.WebApi.Features.CashFlows.DailyCashFlow.Reports
{
    public class ExportDailyCashFlowReportRequest
    {
        public DateOnly? DateFrom { get; set; }
        public DateOnly? DateTo { get; set; }
    }
}
