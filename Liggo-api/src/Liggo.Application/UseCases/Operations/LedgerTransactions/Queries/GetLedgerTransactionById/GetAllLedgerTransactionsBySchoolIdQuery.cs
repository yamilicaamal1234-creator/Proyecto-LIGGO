using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.LedgerTransactions.Queries.GetLedgerTransactionById;
using Liggo.Application.UseCases.Operations.LedgerTransactions.Commands.CreateLedgerTransaction;

namespace Liggo.Application.UseCases.Operations.LedgerTransactions.Queries.GetAllLedgerTransactionsBySchoolId;

public record GetAllLedgerTransactionsBySchoolIdQuery(string SchoolId) : IRequest<IEnumerable<LedgerTransactionResponse>>;

public class GetAllLedgerTransactionsBySchoolIdValidator : FluentValidation.AbstractValidator<GetAllLedgerTransactionsBySchoolIdQuery>
{
    public GetAllLedgerTransactionsBySchoolIdValidator() => RuleFor(x => x.SchoolId).NotEmpty();
}

public class GetAllLedgerTransactionsBySchoolIdHandler : IRequestHandler<GetAllLedgerTransactionsBySchoolIdQuery, IEnumerable<LedgerTransactionResponse>>
{
    private readonly ILedgerTransactionRepository _repository;
    public GetAllLedgerTransactionsBySchoolIdHandler(ILedgerTransactionRepository repository) => _repository = repository;

    public async Task<IEnumerable<LedgerTransactionResponse>> Handle(GetAllLedgerTransactionsBySchoolIdQuery request, CancellationToken cancellationToken)
    {
        var transactions = await _repository.GetAllBySchoolIdAsync(request.SchoolId, cancellationToken);
        
        return transactions.Select(t => new LedgerTransactionResponse(
            t.Id, t.Type, t.Amount, t.Concept, t.Method, t.TransactionRef,
            new TransactionRelatedUsersDto(t.RelatedUsers.PayerName, t.RelatedUsers.StudentName), t.CreatedAt));
    }
}