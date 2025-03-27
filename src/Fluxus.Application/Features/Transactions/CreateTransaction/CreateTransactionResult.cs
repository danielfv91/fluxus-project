namespace Fluxus.Application.Features.Transactions.CreateTransaction
{
    public class CreateTransactionResult
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
