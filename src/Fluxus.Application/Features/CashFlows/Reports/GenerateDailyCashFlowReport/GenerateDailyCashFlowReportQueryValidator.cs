using FluentValidation;

namespace Fluxus.Application.Features.CashFlows.DailyCashFlow.Reports.GenerateDailyCashFlowReport
{
    public class GenerateDailyCashFlowReportQueryValidator : AbstractValidator<GenerateDailyCashFlowReportQuery>
    {
        public GenerateDailyCashFlowReportQueryValidator()
        {
            RuleFor(x => x)
                .Must(x => !x.DateTo.HasValue || x.DateFrom <= x.DateTo)
                .WithMessage("A data inicial deve ser anterior ou igual à data final.");
        }
    }
}
