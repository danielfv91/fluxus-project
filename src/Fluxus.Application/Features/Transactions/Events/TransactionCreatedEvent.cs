using Fluxus.Domain.Enums;
using MediatR;

namespace Fluxus.Application.Features.Transactions.Events
{
    public class TransactionCreatedEvent : INotification
    {
        public Guid UserId { get; }
        public DateOnly Date { get; }
        public decimal Amount { get; }
        public TransactionType Type { get; }

        public TransactionCreatedEvent(Guid userId, DateOnly date, decimal amount, TransactionType type)
        {
            UserId = userId;
            Date = date;
            Amount = amount;
            Type = type;
        }
    }
}
