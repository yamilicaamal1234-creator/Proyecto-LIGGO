using FluentValidation;

namespace Liggo.Application.UseCases.Operations.LedgerTransactions.Commands.CreateLedgerTransaction;

public class CreateLedgerTransactionValidator : AbstractValidator<CreateLedgerTransactionCommand>
{
    public CreateLedgerTransactionValidator()
    {
        RuleFor(x => x.Type)
            .Must(x => x == "charge" || x == "payment")
            .WithMessage("El tipo de transacción solo puede ser 'charge' o 'payment'.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("El monto de la transacción debe ser mayor a cero.");

        // Validaciones Condicionales
        When(x => x.Type == "charge", () =>
        {
            RuleFor(x => x.Concept).NotEmpty().WithMessage("El concepto es obligatorio para los cargos.");
        });

        When(x => x.Type == "payment", () =>
        {
            RuleFor(x => x.Method).NotEmpty().WithMessage("El método de pago es obligatorio.");
            RuleFor(x => x.TransactionRef).NotEmpty().WithMessage("La referencia de transacción es obligatoria para los pagos.");
        });

        RuleFor(x => x.RelatedUsers).NotNull().WithMessage("Los usuarios relacionados son obligatorios.");
        When(x => x.RelatedUsers != null, () =>
        {
            RuleFor(x => x.RelatedUsers.StudentName).NotEmpty().WithMessage("El nombre del alumno es obligatorio.");
            RuleFor(x => x.RelatedUsers.PayerName).NotEmpty().WithMessage("El nombre del tutor/pagador es obligatorio.");
        });
    }
}