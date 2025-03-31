using Fluxus.Application.Features.CashFlows.DailyCashFlow;
using FluentValidation.TestHelper;
using Xunit;

namespace Fluxus.UnitTests.Application.Features.CashFlows.DailyCashFlow
{
    public class DailyCashFlowValidatorTests
    {
        private readonly DailyCashFlowValidator _validator;

        public DailyCashFlowValidatorTests()
        {
            _validator = new DailyCashFlowValidator();
        }

        [Fact]
        public void Should_Have_Validation_Error_When_DateFrom_Is_Greater_Than_DateTo()
        {
            // Arrange
            var request = new DailyCashFlowQuery
            {
                UserId = Guid.NewGuid(),
                DateFrom = new DateOnly(2025, 03, 10),
                DateTo = new DateOnly(2025, 03, 05)
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.DateFrom)
                  .WithErrorMessage("A data inicial deve ser anterior ou igual à data final.");
        }

        [Fact]
        public void Should_Not_Have_Validation_Error_When_Dates_Are_Valid()
        {
            // Arrange
            var request = new DailyCashFlowQuery
            {
                UserId = Guid.NewGuid(),
                DateFrom = new DateOnly(2025, 03, 05),
                DateTo = new DateOnly(2025, 03, 10)
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.DateFrom);
            result.ShouldNotHaveValidationErrorFor(x => x.DateTo);
        }

        [Fact]
        public void Should_Allow_Empty_Dates()
        {
            // Arrange
            var request = new DailyCashFlowQuery
            {
                UserId = Guid.NewGuid()
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
