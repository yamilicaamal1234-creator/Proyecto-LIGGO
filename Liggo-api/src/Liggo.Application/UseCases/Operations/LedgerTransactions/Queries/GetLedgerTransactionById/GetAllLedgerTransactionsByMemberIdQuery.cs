using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.LedgerTransactions.Queries.GetLedgerTransactionById;
using Liggo.Application.UseCases.Operations.LedgerTransactions.Commands.CreateLedgerTransaction;

namespace Liggo.Application.UseCases.Operations.LedgerTransactions.Queries.GetAllLedgerTransactionsByMemberId;

public record GetAllLedgerTransactionsByMemberIdQuery(string MemberId) : IRequest<IEnumerable<LedgerTransactionResponse>>;

public class GetAllLedgerTransactionsByMemberIdValidator : FluentValidation.AbstractValidator<GetAllLedgerTransactionsByMemberIdQuery>
{
    public GetAllLedgerTransactionsByMemberIdValidator() => RuleFor(x => x.MemberId).NotEmpty();
}

public class GetAllLedgerTransactionsByMemberIdHandler : IRequestHandler<GetAllLedgerTransactionsByMemberIdQuery, IEnumerable<LedgerTransactionResponse>>
{
    private readonly ILedgerTransactionRepository _repository;
    public GetAllLedgerTransactionsByMemberIdHandler(ILedgerTransactionRepository repository) => _repository = repository;

    public async Task<IEnumerable<LedgerTransactionResponse>> Handle(GetAllLedgerTransactionsByMemberIdQuery request, CancellationToken cancellationToken)
    {
        var transactions = await _repository.GetAllByMemberIdAsync(request.MemberId, cancellationToken);
        
        return transactions.Select(t => new LedgerTransactionResponse(
            t.Id, t.Type, t.Amount, t.Concept, t.Method, t.TransactionRef,
            new TransactionRelatedUsersDto(t.RelatedUsers.PayerName, t.RelatedUsers.StudentName), t.CreatedAt));
    }
}