namespace Fluxus.Application.Features.CashFlows.DailyCashFlow.Reports.GenerateDailyCashFlowReport
{
    public class GenerateDailyCashFlowReportResult
    {
        public string FileName { get; set; } = string.Empty;
        public byte[] FileContent { get; set; } = Array.Empty<byte>();
        public string ContentType { get; set; } = "application/pdf";
    }
}
