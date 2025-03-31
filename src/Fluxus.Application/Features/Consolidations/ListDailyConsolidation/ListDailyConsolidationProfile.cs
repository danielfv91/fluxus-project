using AutoMapper;
using Fluxus.Domain.Entities;

namespace Fluxus.Application.Features.Consolidations.ListDailyConsolidation
{
    public class ListDailyConsolidationProfile : Profile
    {
        public ListDailyConsolidationProfile()
        {
            CreateMap<DailyConsolidation, ListDailyConsolidationResult>();
        }
    }
}
