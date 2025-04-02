using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Fluxus.Common.Reporting.Constants;

namespace Fluxus.Common.Reporting.Components
{
    public static class ReportHeader
    {
        public static void Create(IContainer container)
        {
            container
                .AlignCenter()
                .Text(ReportMetadata.DailyCashFlowTitle)
                .FontSize(18)
                .Bold()
                .FontColor("#333333");
        }
    }
}
