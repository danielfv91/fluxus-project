using AutoMapper;
using Fluxus.Application.Domain.Repositories;
using MediatR;

namespace Fluxus.Application.Features.CashFlows.DailyCashFlow
{
    public class DailyCashFlowHandler : IRequestHandler<DailyCashFlowQuery, List<DailyCashFlowResult>>
    {
        private readonly IDailyConsolidationRepository _repository;
        private readonly IMapper _mapper;

        public DailyCashFlowHandler(IDailyConsolidationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<DailyCashFlowResult>> Handle(DailyCashFlowQuery request, CancellationToken cancellationToken)
        {
            var from = request.DateFrom;
            var to = request.DateTo;

            if (from.HasValue && !to.HasValue)
                to = DateOnly.FromDateTime(DateTime.UtcNow);

            var consolidations = await _repository.GetAllByUserAsync(request.UserId, cancellationToken);

            var filtered = consolidations
                .Where(c =>
                    (!from.HasValue || c.Date >= from.Value) &&
                    (!to.HasValue || c.Date <= to.Value))
                .OrderBy(c => c.Date)
                .ToList();

            var result = _mapper.Map<List<DailyCashFlowResult>>(filtered);

            decimal runningBalance = 0;
            foreach (var item in result)
            {
                runningBalance += item.DailyBalance;
                item.AccumulatedBalance = runningBalance;
            }

            return result;
        }
    }
}
