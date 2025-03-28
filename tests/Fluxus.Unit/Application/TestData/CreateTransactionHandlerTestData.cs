using System;
using Bogus;
using Fluxus.Application.Features.Transactions.CreateTransaction;
using Fluxus.Domain.Entities;
using Fluxus.Domain.Enums;

namespace Fluxus.Unit.Application.TestData
{
    public static class CreateTransactionHandlerTestData
    {
        private static readonly Faker Faker = new("pt_BR");

        public static CreateTransactionCommand GenerateValidCommand()
        {
            return new CreateTransactionCommand
            {
                Amount = Faker.Finance.Amount(1, 1000),
                Type = Faker.PickRandom<TransactionType>(),
                Description = Faker.Commerce.ProductName(),
                Date = DateTime.Today,
                UserId = Guid.NewGuid()
            };
        }

        public static Transaction GenerateExpectedTransaction(CreateTransactionCommand command)
        {
            return new Transaction
            {
                Id = Guid.NewGuid(),
                Amount = command.Amount,
                Type = command.Type,
                Description = command.Description,
                Date = command.Date,
                UserId = command.UserId
            };
        }
    }
}
