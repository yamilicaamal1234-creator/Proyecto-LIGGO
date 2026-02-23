using FluentValidation;

namespace Liggo.Application.UseCases.Operations.LedgerTransactions.Commands.DeleteLedgerTransaction;

public class DeleteLedgerTransactionValidator : AbstractValidator<DeleteLedgerTransactionCommand>
{
    public DeleteLedgerTransactionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio para eliminar.");
    }
}