using AutoMapper;
using Fluxus.Common.Reporting.Models;
using Fluxus.Domain.Entities;

namespace Fluxus.Application.Features.CashFlows.DailyCashFlow.Reports.GenerateDailyCashFlowReport
{
    public class GenerateDailyCashFlowReportProfile : Profile
    {
        public GenerateDailyCashFlowReportProfile()
        {
            CreateMap<DailyConsolidation, DailyCashFlowReportModel>()
                .ForMember(dest => dest.AccumulatedBalance, opt => opt.Ignore());
        }
    }
}
