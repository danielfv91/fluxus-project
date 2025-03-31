using AutoMapper;
using Fluxus.Application.Features.Consolidations.ListDailyConsolidation;

namespace Fluxus.WebApi.Features.Consolidations.ListDailyConsolidation
{
    public class ListDailyConsolidationProfile : Profile
    {
        public ListDailyConsolidationProfile()
        {
            CreateMap<ListDailyConsolidationRequest, ListDailyConsolidationQuery>();
            CreateMap<ListDailyConsolidationResult, ListDailyConsolidationResponse>();
        }
    }
}
