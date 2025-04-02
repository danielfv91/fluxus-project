using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fluxus.Application.Domain.Repositories;
using Fluxus.Application.Features.Consolidations.ListDailyConsolidation;
using Fluxus.Domain.Entities;
using Fluxus.UnitTests.Application.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Fluxus.UnitTests.Application.Features.Consolidations.ListDailyConsolidation
{
    public class ListDailyConsolidationHandlerTests
    {
        private readonly IDailyConsolidationRepository _repository;
        private readonly IMapper _mapper;
        private readonly ListDailyConsolidationHandler _handler;

        public ListDailyConsolidationHandlerTests()
        {
            _repository = Substitute.For<IDailyConsolidationRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new ListDailyConsolidationHandler(_repository, _mapper);
        }

        [Fact]
        public async Task Should_Return_All_Consolidations_When_No_Date_Filter()
        {
            // Arrange
            var consolidations = ListDailyConsolidationHandlerTestData.GenerateConsolidations();
            var query = ListDailyConsolidationHandlerTestData.CreateQuery();

            _repository
                .GetAllByUserAsync(query.UserId, Arg.Any<CancellationToken>())
                .Returns(consolidations);

            _mapper
                .Map<List<ListDailyConsolidationResult>>(Arg.Any<List<DailyConsolidation>>())
                .Returns(callInfo =>
                {
                    var input = callInfo.ArgAt<List<DailyConsolidation>>(0);
                    return input.Select(c => new ListDailyConsolidationResult
                    {
                        Date = c.Date,
                        TotalCredits = c.TotalCredits,
                        TotalDebits = c.TotalDebits
                    }).ToList();
                });

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().HaveCount(consolidations.Count);
        }

        [Fact]
        public async Task Should_Filter_Consolidations_By_Date_Range()
        {
            // Arrange
            var allConsolidations = ListDailyConsolidationHandlerTestData.GenerateConsolidations();
            var from = new DateOnly(2025, 03, 02);
            var to = new DateOnly(2025, 03, 03);
            var query = ListDailyConsolidationHandlerTestData.CreateQuery(from, to);

            var expected = allConsolidations
                .Where(c => c.Date >= from && c.Date <= to)
                .ToList();

            _repository
                .GetAllByUserAsync(query.UserId, Arg.Any<CancellationToken>())
                .Returns(allConsolidations);

            _mapper
                .Map<List<ListDailyConsolidationResult>>(Arg.Any<List<DailyConsolidation>>())
                .Returns(callInfo =>
                {
                    var input = callInfo.ArgAt<List<DailyConsolidation>>(0);
                    return input.Select(c => new ListDailyConsolidationResult
                    {
                        Date = c.Date,
                        TotalCredits = c.TotalCredits,
                        TotalDebits = c.TotalDebits
                    }).ToList();
                });

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().HaveCount(expected.Count);
            result.All(r => r.Date >= from && r.Date <= to).Should().BeTrue();
        }
    }
}
