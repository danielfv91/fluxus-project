using System;

namespace Fluxus.Application.Features.Transactions.ListTransactions
{
    public class ListTransactionsResult
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public int Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
