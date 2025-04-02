using Fluxus.Common.Reporting.Constants;
using Fluxus.Common.Reporting.Models;
using Fluxus.Common.Reporting.Services.Components;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Fluxus.Common.Reporting.Services.Documents
{
    public class DailyCashFlowReportDocument : IDocument
    {
        private readonly IEnumerable<DailyCashFlowReportModel> _items;

        public DailyCashFlowReportDocument(IEnumerable<DailyCashFlowReportModel> items)
        {
            _items = items;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(40);

                page.Header()
                    .PaddingBottom(20)
                    .Text(ReportMetadata.DailyCashFlowTitle)
                    .SemiBold()
                    .FontSize(18)
                    .AlignCenter();

                page.Content().Component(new DailyCashFlowTableComponent(_items));

                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span("Página ");
                    text.CurrentPageNumber();
                    text.Span(" de ");
                    text.TotalPages();
                });
            });
        }
    }
}
