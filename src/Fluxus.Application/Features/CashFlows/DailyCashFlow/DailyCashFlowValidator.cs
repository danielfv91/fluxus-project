using FluentValidation;

namespace Fluxus.Application.Features.CashFlows.DailyCashFlow
{
    public class DailyCashFlowValidator : AbstractValidator<DailyCashFlowQuery>
    {
        public DailyCashFlowValidator()
        {
            RuleFor(x => x.DateFrom)
                .LessThanOrEqualTo(x => x.DateTo)
                .When(x => x.DateFrom.HasValue && x.DateTo.HasValue)
                .WithMessage("A data inicial deve ser anterior ou igual à data final.");
        }
    }
}
