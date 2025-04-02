using AutoMapper;
using Fluxus.Domain.Entities;

namespace Fluxus.Application.Features.CashFlows.DailyCashFlow
{
    public class DailyCashFlowProfile : Profile
    {
        public DailyCashFlowProfile()
        {
            CreateMap<DailyConsolidation, DailyCashFlowResult>()
                .ForMember(dest => dest.DailyBalance, opt => opt.Ignore())
                .ForMember(dest => dest.AccumulatedBalance, opt => opt.Ignore());
        }
    }
}
