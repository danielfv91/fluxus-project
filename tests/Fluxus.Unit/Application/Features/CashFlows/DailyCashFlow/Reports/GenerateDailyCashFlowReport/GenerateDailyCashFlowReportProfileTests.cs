using AutoMapper;
using Fluxus.Application.Features.CashFlows.DailyCashFlow.Reports.GenerateDailyCashFlowReport;
using Fluxus.Common.Reporting.Models;
using Fluxus.UnitTests.Application.TestData;
using FluentAssertions;
using Xunit;

namespace Fluxus.UnitTests.Application.Features.CashFlows.Reports.GenerateDailyCashFlowReport
{
    public class GenerateDailyCashFlowReportProfileTests
    {
        private readonly IMapper _mapper;

        public GenerateDailyCashFlowReportProfileTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GenerateDailyCashFlowReportProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Should_Map_DailyConsolidation_To_ReportModel()
        {
            // Arrange
            var consolidations = GenerateDailyCashFlowReportTestData.GenerateConsolidations();

            // Act
            var result = _mapper.Map<List<DailyCashFlowReportModel>>(consolidations);

            // Assert
            result.Should().HaveCount(consolidations.Count);

            for (int i = 0; i < consolidations.Count; i++)
            {
                result[i].Date.Should().Be(consolidations[i].Date);
                result[i].TotalCredits.Should().Be(consolidations[i].TotalCredits);
                result[i].TotalDebits.Should().Be(consolidations[i].TotalDebits);
            }
        }
    }
}
