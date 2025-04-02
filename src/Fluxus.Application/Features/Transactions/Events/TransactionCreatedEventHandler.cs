using Fluxus.Application.Domain.Repositories;
using Fluxus.Domain.Entities;
using Fluxus.Domain.Enums;
using MediatR;

namespace Fluxus.Application.Features.Transactions.Events
{
    public class TransactionCreatedEventHandler : INotificationHandler<TransactionCreatedEvent>
    {
        private readonly IDailyConsolidationRepository _repository;

        public TransactionCreatedEventHandler(IDailyConsolidationRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(TransactionCreatedEvent notification, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByUserAndDateAsync(notification.UserId, notification.Date, cancellationToken);

            if (existing is null)
            {
                var newConsolidation = new DailyConsolidation
                {
                    Id = Guid.NewGuid(),
                    UserId = notification.UserId,
                    Date = notification.Date,
                    TotalCredits = notification.Type == TransactionType.Credit ? notification.Amount : 0,
                    TotalDebits = notification.Type == TransactionType.Debit ? notification.Amount : 0
                };

                await _repository.AddAsync(newConsolidation, cancellationToken);
                return;
            }

            if (notification.Type == TransactionType.Credit)
                existing.TotalCredits += notification.Amount;
            else
                existing.TotalDebits += notification.Amount;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
    }
}
