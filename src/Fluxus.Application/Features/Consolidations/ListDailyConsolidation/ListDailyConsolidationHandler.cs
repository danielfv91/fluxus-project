using AutoMapper;
using Fluxus.Application.Domain.Repositories;
using MediatR;

namespace Fluxus.Application.Features.Consolidations.ListDailyConsolidation
{
    public class ListDailyConsolidationHandler : IRequestHandler<ListDailyConsolidationQuery, IEnumerable<DailyConsolidationResult>>
    {
        private readonly IDailyConsolidationRepository _repository;
        private readonly IMapper _mapper;

        public ListDailyConsolidationHandler(IDailyConsolidationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DailyConsolidationResult>> Handle(ListDailyConsolidationQuery request, CancellationToken cancellationToken)
        {
            var all = await _repository.GetAllByUserAsync(request.UserId, cancellationToken);

            var filtered = all
                .Where(d =>
                    (!request.DateFrom.HasValue || d.Date >= request.DateFrom.Value) &&
                    (!request.DateTo.HasValue || d.Date <= request.DateTo.Value))
                .OrderByDescending(d => d.Date);

            return _mapper.Map<IEnumerable<DailyConsolidationResult>>(filtered);
        }

    }
}
