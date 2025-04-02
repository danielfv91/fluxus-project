using QuestPDF.Infrastructure;

namespace Fluxus.Common.Reporting.Interfaces
{
    public interface IPdfReportGenerator
    {
        Task<byte[]> GenerateAsync(IDocument document, CancellationToken cancellationToken);
    }
}
