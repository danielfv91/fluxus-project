using FluentValidation;

namespace Fluxus.Application.Features.Consolidations.ListDailyConsolidation
{
    public class ListDailyConsolidationValidator : AbstractValidator<ListDailyConsolidationQuery>
    {
        public ListDailyConsolidationValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Usuário não identificado.");

            RuleFor(x => x)
                .Must(x => !x.DateFrom.HasValue || !x.DateTo.HasValue || x.DateFrom <= x.DateTo)
                .WithMessage("A data inicial deve ser anterior ou igual à data final.");
        }
    }
}
