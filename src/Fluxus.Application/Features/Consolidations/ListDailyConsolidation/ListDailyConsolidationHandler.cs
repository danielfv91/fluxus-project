using AutoMapper;
using Fluxus.Application.Domain.Repositories;
using MediatR;

namespace Fluxus.Application.Features.Consolidations.ListDailyConsolidation
{
    public class ListDailyConsolidationHandler : IRequestHandler<ListDailyConsolidationQuery, List<ListDailyConsolidationResult>>
    {
        private readonly IDailyConsolidationRepository _repository;
        private readonly IMapper _mapper;

        public ListDailyConsolidationHandler(IDailyConsolidationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ListDailyConsolidationResult>> Handle(ListDailyConsolidationQuery request, CancellationToken cancellationToken)
        {
            var consolidations = await _repository.GetAllByUserAsync(request.UserId, cancellationToken);

            if (request.DateFrom.HasValue)
                consolidations = consolidations
                    .Where(c => c.Date >= request.DateFrom.Value)
                    .ToList();

            if (request.DateTo.HasValue)
                consolidations = consolidations
                    .Where(c => c.Date <= request.DateTo.Value)
                    .ToList();

            return _mapper.Map<List<ListDailyConsolidationResult>>(consolidations);
        }
    }
}
