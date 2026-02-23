using FluentValidation;

namespace Liggo.Application.UseCases.Operations.LedgerTransactions.Queries.GetLedgerTransactionById;

public class GetLedgerTransactionByIdValidator : AbstractValidator<GetLedgerTransactionByIdQuery>
{
    public GetLedgerTransactionByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio.");
    }
}