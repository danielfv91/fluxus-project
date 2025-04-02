using Bogus;
using Fluxus.Application.Features.CashFlows.DailyCashFlow.Reports.GenerateDailyCashFlowReport;
using Fluxus.Common.Reporting.Models;
using Fluxus.Domain.Entities;

namespace Fluxus.UnitTests.Application.TestData
{
    public static class GenerateDailyCashFlowReportTestData
    {
        public static readonly Guid UserId = Guid.NewGuid();

        public static List<DailyConsolidation> GenerateConsolidations()
        {
            return new List<DailyConsolidation>
            {
                new() { Id = Guid.NewGuid(), UserId = UserId, Date = new DateOnly(2025, 03, 01), TotalCredits = 100, TotalDebits = 30 },
                new() { Id = Guid.NewGuid(), UserId = UserId, Date = new DateOnly(2025, 03, 02), TotalCredits = 150, TotalDebits = 50 },
                new() { Id = Guid.NewGuid(), UserId = UserId, Date = new DateOnly(2025, 03, 03), TotalCredits = 80, TotalDebits = 60 }
            };
        }

        public static GenerateDailyCashFlowReportQuery CreateQueryWithRange()
        {
            return new GenerateDailyCashFlowReportQuery
            {
                UserId = UserId,
                DateFrom = new DateOnly(2025, 03, 01),
                DateTo = new DateOnly(2025, 03, 03)
            };
        }

        public static List<DailyCashFlowReportModel> GenerateMappedReportModels()
        {
            var faker = new Faker<DailyCashFlowReportModel>()
                .RuleFor(m => m.Date, f => new DateOnly(2025, 03, f.Random.Int(1, 3)))
                .RuleFor(m => m.TotalCredits, f => f.Finance.Amount(50, 200))
                .RuleFor(m => m.TotalDebits, f => f.Finance.Amount(10, 100))
                .RuleFor(m => m.AccumulatedBalance, 0);

            return faker.Generate(3).OrderBy(x => x.Date).ToList();
        }
    }
}
