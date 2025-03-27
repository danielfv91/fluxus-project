using Fluxus.Domain.Enums;

namespace Fluxus.WebApi.Features.Transactions.CreateTransactionFeature
{
    public class CreateTransactionRequest
    {
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
