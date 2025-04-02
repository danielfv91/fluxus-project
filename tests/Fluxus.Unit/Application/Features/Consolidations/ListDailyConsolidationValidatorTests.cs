using System;
using System.Threading;
using System.Threading.Tasks;
using Fluxus.Application.Features.Consolidations.ListDailyConsolidation;
using FluentAssertions;
using Xunit;

namespace Fluxus.UnitTests.Application.Features.Consolidations.ListDailyConsolidation
{
    public class ListDailyConsolidationValidatorTests
    {
        private readonly ListDailyConsolidationValidator _validator;

        public ListDailyConsolidationValidatorTests()
        {
            _validator = new ListDailyConsolidationValidator();
        }

        [Fact]
        public async Task Should_Fail_When_UserId_Is_Empty()
        {
            // Arrange
            var query = new ListDailyConsolidationQuery
            {
                UserId = Guid.Empty
            };

            // Act
            var result = await _validator.ValidateAsync(query, CancellationToken.None);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "UserId");
        }

        [Fact]
        public async Task Should_Fail_When_DateFrom_Is_Greater_Than_DateTo()
        {
            // Arrange
            var query = new ListDailyConsolidationQuery
            {
                UserId = Guid.NewGuid(),
                DateFrom = new DateOnly(2025, 03, 05),
                DateTo = new DateOnly(2025, 03, 01)
            };

            // Act
            var result = await _validator.ValidateAsync(query, CancellationToken.None);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "A data inicial deve ser anterior ou igual à data final.");

        }

        [Fact]
        public async Task Should_Pass_When_Valid_Input()
        {
            // Arrange
            var query = new ListDailyConsolidationQuery
            {
                UserId = Guid.NewGuid(),
                DateFrom = new DateOnly(2025, 03, 01),
                DateTo = new DateOnly(2025, 03, 10)
            };

            // Act
            var result = await _validator.ValidateAsync(query, CancellationToken.None);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}
