using Fluxus.Common.Reporting.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Fluxus.Common.Reporting.Services
{
    public class PdfReportGenerator : IPdfReportGenerator
    {
        public Task<byte[]> GenerateAsync(IDocument document, CancellationToken cancellationToken = default)
        {
            var pdfBytes = document.GeneratePdf();
            return Task.FromResult(pdfBytes);
        }
    }
}
