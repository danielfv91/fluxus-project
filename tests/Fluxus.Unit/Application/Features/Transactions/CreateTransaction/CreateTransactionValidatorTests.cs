using System;
using Bogus;
using FluentAssertions;
using Fluxus.Application.Features.Transactions.CreateTransaction;
using Fluxus.Domain.Enums;
using Xunit;

namespace Fluxus.UnitTests.Application.Features.Transactions.CreateTransaction
{
    public class CreateTransactionValidatorTests
    {
        private readonly CreateTransactionValidator _validator;
        private readonly Faker _faker;

        public CreateTransactionValidatorTests()
        {
            _validator = new CreateTransactionValidator();
            _faker = new Faker("pt_BR");
        }

        [Fact]
        public void Should_Have_Error_When_Date_Is_In_The_Future()
        {
            // Arrange
            var command = new CreateTransactionCommand
            {
                Amount = 100,
                Type = TransactionType.Credit,
                Description = _faker.Lorem.Sentence(),
                Date = DateTime.Today.AddDays(1),
                UserId = Guid.NewGuid()
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e =>
                e.PropertyName == "Date" &&
                e.ErrorMessage == "A data da transação não pode ser no futuro.");
        }

        [Fact]
        public void Should_Not_Have_Errors_When_Date_Is_Today_Or_Past()
        {
            // Arrange
            var command = new CreateTransactionCommand
            {
                Amount = 50,
                Type = TransactionType.Debit,
                Description = _faker.Commerce.ProductName(),
                Date = DateTime.Today,
                UserId = Guid.NewGuid()
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}
