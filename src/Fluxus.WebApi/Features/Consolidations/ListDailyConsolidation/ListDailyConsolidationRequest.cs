using Microsoft.AspNetCore.Mvc;

namespace Fluxus.WebApi.Features.Consolidations.ListDailyConsolidation
{
    public class ListDailyConsolidationRequest
    {
        [FromQuery(Name = "DateFrom")]
        public DateOnly? DateFrom { get; set; }

        [FromQuery(Name = "DateTo")]
        public DateOnly? DateTo { get; set; }
    }
}
