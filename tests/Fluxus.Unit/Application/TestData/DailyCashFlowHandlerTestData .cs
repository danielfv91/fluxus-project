using Fluxus.Application.Features.CashFlows.DailyCashFlow;
using Fluxus.Domain.Entities;

namespace Fluxus.UnitTests.Application.TestData
{
    public static class DailyCashFlowHandlerTestData
    {
        public static readonly Guid UserId = Guid.NewGuid();

        public static List<DailyConsolidation> GenerateConsolidations()
        {
            return new List<DailyConsolidation>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    Date = new DateOnly(2025, 03, 06),
                    TotalCredits = 100,
                    TotalDebits = 150
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    Date = new DateOnly(2025, 03, 07),
                    TotalCredits = 200,
                    TotalDebits = 50
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    Date = new DateOnly(2025, 03, 08),
                    TotalCredits = 0,
                    TotalDebits = 100
                }
            };
        }

        public static DailyCashFlowQuery CreateQuery(DateOnly? from = null, DateOnly? to = null)
        {
            return new DailyCashFlowQuery
            {
                UserId = UserId,
                DateFrom = from,
                DateTo = to
            };
        }
    }
}
