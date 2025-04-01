using QuestPDF.Infrastructure;

namespace Fluxus.Common.Reporting.Interfaces
{
    public interface IPdfReportDocument : IDocument
    {
        byte[] GeneratePdf();
    }
}
