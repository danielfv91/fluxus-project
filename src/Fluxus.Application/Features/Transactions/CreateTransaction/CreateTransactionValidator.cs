using FluentValidation;

namespace Fluxus.Application.Features.Transactions.CreateTransaction
{
    public class CreateTransactionValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionValidator()
        {
            Console.WriteLine(" Validator executado!");

            RuleFor(x => x.Date)
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("A data da transação não pode ser no futuro.");

        }
    }
}
