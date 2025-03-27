using FluentValidation;

namespace Fluxus.WebApi.Features.Transactions.CreateTransactionFeature
{
    public class CreateTransactionRequestValidator : AbstractValidator<CreateTransactionRequest>
    {
        public CreateTransactionRequestValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("O valor deve ser maior que zero.");

            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage("O tipo da transação deve ser válido (1 = Crédito, 2 = Débito).");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("A descrição é obrigatória.")
                .MaximumLength(200).WithMessage("A descrição deve ter no máximo 200 caracteres.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("A data é obrigatória.");
        }
    }
}
