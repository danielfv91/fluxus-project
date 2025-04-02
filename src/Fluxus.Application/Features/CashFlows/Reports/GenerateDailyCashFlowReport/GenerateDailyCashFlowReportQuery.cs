using MediatR;

namespace Fluxus.Application.Features.CashFlows.DailyCashFlow.Reports.GenerateDailyCashFlowReport
{
    public class GenerateDailyCashFlowReportQuery : IRequest<GenerateDailyCashFlowReportResult>
    {
        public Guid UserId { get; set; }
        public DateOnly? DateFrom { get; set; }
        public DateOnly? DateTo { get; set; }
    }
}
