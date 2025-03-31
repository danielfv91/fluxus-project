using Fluxus.Application.Features.Consolidations.ListDailyConsolidation;
using Fluxus.Domain.Entities;

namespace Fluxus.UnitTests.Application.TestData
{
    public static class ListDailyConsolidationHandlerTestData
    {
        public static Guid UserId = Guid.NewGuid();

        public static List<DailyConsolidation> GenerateConsolidations()
        {
            return new List<DailyConsolidation>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    Date = new DateOnly(2025, 03, 01),
                    TotalCredits = 100,
                    TotalDebits = 30
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    Date = new DateOnly(2025, 03, 02),
                    TotalCredits = 150,
                    TotalDebits = 50
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    Date = new DateOnly(2025, 03, 03),
                    TotalCredits = 80,
                    TotalDebits = 60
                }
            };
        }

        public static ListDailyConsolidationQuery CreateQuery(DateOnly? from = null, DateOnly? to = null)
        {
            return new ListDailyConsolidationQuery
            {
                UserId = UserId,
                DateFrom = from,
                DateTo = to
            };
        }
    }
}
