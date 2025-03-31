using AutoMapper;
using Fluxus.Application.Domain.Repositories;
using Fluxus.Application.Domain.UnitOfWork;
using Fluxus.Application.Features.Transactions.Events;
using Fluxus.Domain.Entities;
using MediatR;

namespace Fluxus.Application.Features.Transactions.CreateTransaction
{
    public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, CreateTransactionResult>
    {
        private readonly ITransactionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPublisher _publisher;
        private readonly IUnitOfWork _unitOfWork;
        public CreateTransactionHandler(
            ITransactionRepository repository,
            IMapper mapper,
            IPublisher publisher,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _publisher = publisher;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateTransactionResult> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = _mapper.Map<Transaction>(request);

            transaction.Date = DateTime.SpecifyKind(transaction.Date, DateTimeKind.Utc);

            await _repository.AddAsync(transaction, cancellationToken);

            await _publisher.Publish(new TransactionCreatedEvent(
                request.UserId,
                DateOnly.FromDateTime(request.Date),
                request.Amount,
                request.Type
            ), cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CreateTransactionResult>(transaction);
        }
    }
}
