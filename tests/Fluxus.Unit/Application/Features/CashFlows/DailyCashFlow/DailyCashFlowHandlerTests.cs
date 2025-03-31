using AutoMapper;
using Fluxus.Application.Domain.Repositories;
using Fluxus.Application.Features.CashFlows.DailyCashFlow;
using Fluxus.Domain.Entities;
using Fluxus.UnitTests.Application.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Fluxus.UnitTests.Application.Features.CashFlows.DailyCashFlow
{
    public class DailyCashFlowHandlerTests
    {
        private readonly IDailyConsolidationRepository _repository;
        private readonly IMapper _mapper;
        private readonly DailyCashFlowHandler _handler;

        public DailyCashFlowHandlerTests()
        {
            _repository = Substitute.For<IDailyConsolidationRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new DailyCashFlowHandler(_repository, _mapper);
        }

        [Fact]
        public async Task Should_Calculate_Daily_And_Accumulated_Balance_Correctly()
        {
            // Arrange
            var consolidations = DailyCashFlowHandlerTestData.GenerateConsolidations();
            var query = DailyCashFlowHandlerTestData.CreateQuery();

            _repository
                .GetAllByUserAsync(query.UserId, Arg.Any<CancellationToken>())
                .Returns(consolidations);

            _mapper
                .Map<List<DailyCashFlowResult>>(Arg.Any<List<DailyConsolidation>>())
                .Returns(callInfo =>
                {
                    var input = callInfo.ArgAt<List<DailyConsolidation>>(0);
                    return input.Select(c => new DailyCashFlowResult
                    {
                        Date = c.Date,
                        TotalCredits = c.TotalCredits,
                        TotalDebits = c.TotalDebits
                    }).ToList();
                });

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().HaveCount(3);

            result[0].DailyBalance.Should().Be(-50);
            result[0].AccumulatedBalance.Should().Be(-50);

            result[1].DailyBalance.Should().Be(150);
            result[1].AccumulatedBalance.Should().Be(100);

            result[2].DailyBalance.Should().Be(-100);
            result[2].AccumulatedBalance.Should().Be(0);
        }
    }
}
