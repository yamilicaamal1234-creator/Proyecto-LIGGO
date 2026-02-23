using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.LedgerTransactions.Commands.CreateLedgerTransaction;
using Liggo.Application.UseCases.Operations.LedgerTransactions.Queries.Responses;

namespace Liggo.Application.UseCases.Operations.LedgerTransactions.Queries.GetAllLedgerTransactionsByMemberId;

public class GetAllLedgerTransactionsByMemberIdHandler : IRequestHandler<GetAllLedgerTransactionsByMemberIdQuery, IEnumerable<LedgerTransactionResponse>>
{
    private readonly ILedgerTransactionRepository _repository;

    public GetAllLedgerTransactionsByMemberIdHandler(ILedgerTransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<LedgerTransactionResponse>> Handle(GetAllLedgerTransactionsByMemberIdQuery request, CancellationToken cancellationToken)
    {
        var transactions = await _repository.GetAllByMemberIdAsync(request.MemberId, cancellationToken);
        
        if (transactions == null || !transactions.Any())
            return Enumerable.Empty<LedgerTransactionResponse>();
            
        return transactions.Select(t => new LedgerTransactionResponse(
            t.Id, 
            t.Type, 
            t.Amount, 
            t.Concept, 
            t.Method, 
            t.TransactionRef,
            new TransactionRelatedUsersDto(t.RelatedUsers.PayerName, t.RelatedUsers.StudentName), 
            t.CreatedAt));
    }
}