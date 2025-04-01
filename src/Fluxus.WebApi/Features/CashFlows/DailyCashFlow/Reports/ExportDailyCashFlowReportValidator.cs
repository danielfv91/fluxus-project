using FluentValidation;

namespace Fluxus.WebApi.Features.CashFlows.DailyCashFlow.Reports
{
    public class ExportDailyCashFlowReportValidator : AbstractValidator<ExportDailyCashFlowReportRequest>
    {
        public ExportDailyCashFlowReportValidator()
        {
            RuleFor(x => x.DateFrom)
                .LessThanOrEqualTo(x => x.DateTo)
                .When(x => x.DateFrom.HasValue && x.DateTo.HasValue)
                .WithMessage("A data inicial deve ser anterior ou igual à data final.");

            RuleFor(x => x.DateTo)
                .Null()
                .When(x => x.DateFrom is null)
                .WithMessage("A data final não pode ser informada sem uma data inicial.");

        }
    }
}
