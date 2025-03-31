using FluentValidation;

namespace Fluxus.WebApi.Features.CashFlows.DailyCashFlow
{
    public class DailyCashFlowRequestValidator : AbstractValidator<DailyCashFlowRequest>
    {
        public DailyCashFlowRequestValidator()
        {
            RuleFor(x => x.DateFrom)
                .LessThanOrEqualTo(x => x.DateTo)
                .When(x => x.DateFrom.HasValue && x.DateTo.HasValue)
                .WithMessage("A data inicial deve ser anterior ou igual à data final.");

            RuleFor(x => x.DateTo)
                .Must((request, dateTo) =>
                    dateTo == null || request.DateFrom != null)
                .WithMessage("DateFrom deve ser informado se DateTo for utilizado.");
        }
    }
}
