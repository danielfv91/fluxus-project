namespace Fluxus.Common.Reporting.Models
{
    public class PdfReportRequest
    {
        public string TemplatePath { get; set; } = string.Empty;
        public object Data { get; set; } = default!;
        public string Title { get; set; } = "Relatório";
    }
}
