using AutoMapper;
using Fluxus.Application.Domain.Repositories;
using Fluxus.Domain.Entities;
using MediatR;

namespace Fluxus.Application.Features.Transactions.CreateTransaction
{
    public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, CreateTransactionResult>
    {
        private readonly ITransactionRepository _repository;
        private readonly IMapper _mapper;

        public CreateTransactionHandler(ITransactionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CreateTransactionResult> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = _mapper.Map<Transaction>(request);

            await _repository.AddAsync(transaction, cancellationToken);

            return _mapper.Map<CreateTransactionResult>(transaction);
        }
    }
}
