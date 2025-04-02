using AutoMapper;
using Fluxus.Application.Features.CashFlows.DailyCashFlow;
using Fluxus.WebApi.Features.CashFlows.DailyCashFlow;

namespace Fluxus.WebApi.Features.CashFlows.DailyCashFlow
{
    public class DailyCashFlowProfile : Profile
    {
        public DailyCashFlowProfile()
        {
            CreateMap<DailyCashFlowRequest, DailyCashFlowQuery>();

            CreateMap<DailyCashFlowResult, DailyCashFlowResponse>();
        }
    }
}
