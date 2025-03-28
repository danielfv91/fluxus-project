using FluentValidation;

namespace Fluxus.WebApi.Features.Transactions.ListTransactionsFeature
{
    public class ListTransactionsRequestValidator : AbstractValidator<ListTransactionsRequest>
    {
        public ListTransactionsRequestValidator()
        {
            RuleFor(x => x.DateFrom)
                .LessThanOrEqualTo(x => x.DateTo)
                .When(x => x.DateFrom.HasValue && x.DateTo.HasValue)
                .WithMessage("A data inicial deve ser menor ou igual à data final.");
        }
    }
}
