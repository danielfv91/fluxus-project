using Fluxus.Domain.Enums;
using MediatR;

namespace Fluxus.Application.Features.Transactions.CreateTransaction
{
    public class CreateTransactionCommand : IRequest<CreateTransactionResult>
    {
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
    }
}
