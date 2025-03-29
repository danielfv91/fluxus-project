using FluentValidation;

namespace Fluxus.WebApi.Features.Consolidations.ListDailyConsolidation
{
    public class ListDailyConsolidationRequestValidator : AbstractValidator<ListDailyConsolidationRequest>
    {
        public ListDailyConsolidationRequestValidator()
        {
            RuleFor(x => x)
                .Must(x => !x.DateFrom.HasValue || !x.DateTo.HasValue || x.DateFrom <= x.DateTo)
                .WithMessage("A data inicial deve ser anterior ou igual à data final.");
        }
    }
}
