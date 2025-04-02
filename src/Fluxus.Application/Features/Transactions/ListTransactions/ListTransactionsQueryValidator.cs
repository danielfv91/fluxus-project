using FluentValidation;

namespace Fluxus.Application.Features.Transactions.ListTransactions
{
    public class ListTransactionsQueryValidator : AbstractValidator<ListTransactionsQuery>
    {
        public ListTransactionsQueryValidator()
        {
            RuleFor(x => x.DateFrom)
                .LessThanOrEqualTo(x => x.DateTo)
                .When(x => x.DateFrom.HasValue && x.DateTo.HasValue)
                .WithMessage("A data inicial deve ser menor ou igual à data final.");
        }
    }
}
