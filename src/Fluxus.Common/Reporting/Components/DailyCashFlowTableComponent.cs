using Fluxus.Common.Reporting.Constants;
using Fluxus.Common.Reporting.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Fluxus.Common.Reporting.Services.Components
{
    public class DailyCashFlowTableComponent : IComponent
    {
        private readonly IEnumerable<DailyCashFlowReportModel> _items;

        public DailyCashFlowTableComponent(IEnumerable<DailyCashFlowReportModel> items)
        {
            _items = items;
        }

        public void Compose(IContainer container)
        {
            container
                .Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(2); 
                        columns.RelativeColumn();  
                        columns.RelativeColumn();  
                        columns.RelativeColumn();  
                        columns.RelativeColumn();  
                    });

                    // Header
                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text(ReportLabels.Date).SemiBold();
                        header.Cell().Element(CellStyle).Text(ReportLabels.Credits).SemiBold();
                        header.Cell().Element(CellStyle).Text(ReportLabels.Debits).SemiBold();
                        header.Cell().Element(CellStyle).Text(ReportLabels.DailyBalance).SemiBold();
                        header.Cell().Element(CellStyle).Text(ReportLabels.AccumulatedBalance).SemiBold();
                    });

                    // Data
                    foreach (var item in _items)
                    {
                        table.Cell().Element(CellStyle).Text(item.Date.ToString("dd/MM/yyyy"));
                        table.Cell().Element(CellStyle).Text(item.TotalCredits.ToString("C"));
                        table.Cell().Element(CellStyle).Text(item.TotalDebits.ToString("C"));
                        table.Cell().Element(CellStyle).Text(item.DailyBalance.ToString("C"));
                        table.Cell().Element(CellStyle).Text(item.AccumulatedBalance.ToString("C"));
                    }

                    static IContainer CellStyle(IContainer container)
                    {
                        return container
                            .Padding(5)
                            .BorderBottom(1)
                            .BorderColor(Colors.Grey.Lighten2);
                    }
                });
        }
    }
}
