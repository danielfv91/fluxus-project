namespace Fluxus.Common.Reporting.Models
{
    public class PdfReportSection
    {
        public string Header { get; set; } = string.Empty;
        public Dictionary<string, string> Rows { get; set; } = new();
    }
}
