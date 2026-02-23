using FluentValidation;

namespace Liggo.Application.UseCases.Operations.LedgerTransactions.Queries.GetAllLedgerTransactionsBySchoolId;

public class GetAllLedgerTransactionsBySchoolIdValidator : AbstractValidator<GetAllLedgerTransactionsBySchoolIdQuery>
{
    public GetAllLedgerTransactionsBySchoolIdValidator()
    {
        RuleFor(x => x.SchoolId).NotEmpty().WithMessage("El ID de la escuela es obligatorio.");
    }
}