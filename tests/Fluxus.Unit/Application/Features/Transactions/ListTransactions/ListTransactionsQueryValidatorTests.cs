using Fluxus.Application.Features.Transactions.ListTransactions;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Fluxus.UnitTests.Application.Features.Transactions.ListTransactions
{
    public class ListTransactionsQueryValidatorTests
    {
        private readonly ListTransactionsQueryValidator _validator = new();

        [Fact]
        public void Should_Pass_When_Dates_Are_Null()
        {
            // Arrange
            var query = new ListTransactionsQuery();

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_Pass_When_DateFrom_Is_Less_Than_Or_Equal_To_DateTo()
        {
            var query = new ListTransactionsQuery
            {
                DateFrom = new DateTime(2025, 03, 01),
                DateTo = new DateTime(2025, 03, 31)
            };

            var result = _validator.TestValidate(query);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_Fail_When_DateFrom_Is_Greater_Than_DateTo()
        {
            var query = new ListTransactionsQuery
            {
                DateFrom = new DateTime(2025, 04, 01),
                DateTo = new DateTime(2025, 03, 01)
            };

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(q => q.DateFrom)
                .WithErrorMessage("A data inicial deve ser menor ou igual à data final.");
        }
    }
}
