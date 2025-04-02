namespace Fluxus.WebApi.Features.CashFlows.DailyCashFlow.Reports
{
    public class ExportDailyCashFlowReportResponse
    {
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = "application/pdf";
        public byte[] FileContent { get; set; } = [];
    }
}