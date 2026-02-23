using FluentValidation;

namespace Liggo.Application.UseCases.Operations.LedgerTransactions.Queries.GetAllLedgerTransactionsByMemberId;

public class GetAllLedgerTransactionsByMemberIdValidator : AbstractValidator<GetAllLedgerTransactionsByMemberIdQuery>
{
    public GetAllLedgerTransactionsByMemberIdValidator()
    {
        RuleFor(x => x.MemberId).NotEmpty().WithMessage("El ID del miembro/alumno es obligatorio.");
    }
}