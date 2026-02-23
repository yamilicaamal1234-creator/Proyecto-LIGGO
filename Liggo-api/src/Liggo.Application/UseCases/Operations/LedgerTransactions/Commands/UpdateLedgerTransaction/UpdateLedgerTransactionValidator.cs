using FluentValidation;

namespace Liggo.Application.UseCases.Operations.LedgerTransactions.Commands.UpdateLedgerTransaction;

public class UpdateLedgerTransactionValidator : AbstractValidator<UpdateLedgerTransactionCommand>
{
    public UpdateLedgerTransactionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID de la transacción es obligatorio.");
        RuleFor(x => x.Type).Must(x => x == "charge" || x == "payment").WithMessage("Tipo inválido.");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("El monto debe ser mayor a cero.");

        When(x => x.Type == "charge", () => RuleFor(x => x.Concept).NotEmpty());
        When(x => x.Type == "payment", () => 
        {
            RuleFor(x => x.Method).NotEmpty();
            RuleFor(x => x.TransactionRef).NotEmpty();
        });

        RuleFor(x => x.RelatedUsers).NotNull();
    }
}