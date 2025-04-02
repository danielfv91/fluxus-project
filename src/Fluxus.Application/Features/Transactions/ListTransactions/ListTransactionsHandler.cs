using AutoMapper;
using Fluxus.Application.Domain.Repositories;
using Fluxus.Common.Security.Interfaces;
using MediatR;

namespace Fluxus.Application.Features.Transactions.ListTransactions
{
    public class ListTransactionsHandler : IRequestHandler<ListTransactionsQuery, List<ListTransactionsResult>>
    {
        private readonly ITransactionRepository _repository;
        private readonly IAuthenticatedUser _authenticatedUser;
        private readonly IMapper _mapper;

        public ListTransactionsHandler(
            ITransactionRepository repository,
            IAuthenticatedUser authenticatedUser,
            IMapper mapper)
        {
            _repository = repository;
            _authenticatedUser = authenticatedUser;
            _mapper = mapper;
        }

        public async Task<List<ListTransactionsResult>> Handle(ListTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _repository.GetByPeriodAsync(
                _authenticatedUser.Id,
                request.DateFrom,
                request.DateTo,
                cancellationToken);

            return _mapper.Map<List<ListTransactionsResult>>(transactions);
        }
    }
}
