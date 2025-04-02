using AutoMapper;
using Fluxus.Application.Features.CashFlows.DailyCashFlow.Reports.GenerateDailyCashFlowReport;

namespace Fluxus.WebApi.Features.CashFlows.DailyCashFlow.Reports
{
    public class ExportDailyCashFlowReportProfile : Profile
    {
        public ExportDailyCashFlowReportProfile()
        {
            CreateMap<ExportDailyCashFlowReportRequest, GenerateDailyCashFlowReportQuery>();
        }
    }
}
