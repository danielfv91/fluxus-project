using Bogus;
using Fluxus.Application.Features.Transactions.ListTransactions;
using Fluxus.Domain.Entities;
using Fluxus.Domain.Enums;

namespace Fluxus.UnitTests.TestData
{
    public static class ListTransactionsHandlerTestData
    {
        public static readonly Guid UserId = Guid.NewGuid();

        public static List<Transaction> GenerateFakeTransactions()
        {
            return new Faker<Transaction>()
                .RuleFor(t => t.Id, f => f.Random.Guid())
                .RuleFor(t => t.UserId, _ => UserId)
                .RuleFor(t => t.Amount, f => f.Finance.Amount(1, 500))
                .RuleFor(t => t.Type, f => f.PickRandom<TransactionType>())
                .RuleFor(t => t.Description, f => f.Commerce.ProductName())
                .RuleFor(t => t.Date, f => f.Date.Between(DateTime.Today.AddDays(-10), DateTime.Today))
                .Generate(5);
        }

        public static ListTransactionsQuery GetQuery(DateTime? from = null, DateTime? to = null)
        {
            return new ListTransactionsQuery
            {
                DateFrom = from,
                DateTo = to
            };
        }
    }
}
