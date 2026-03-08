using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations; // ¡Este using faltaba!
using Liggo.Application.UseCases.Operations.LedgerTransactions.Commands.CreateLedgerTransaction;
using Liggo.Application.UseCases.Operations.LedgerTransactions.Queries.Responses;

namespace Liggo.Application.UseCases.Operations.LedgerTransactions.Queries.GetLedgerTransactionById;

public class GetLedgerTransactionByIdHandler : IRequestHandler<GetLedgerTransactionByIdQuery, LedgerTransactionResponse?>
{
    private readonly ILedgerTransactionRepository _repository;

    public GetLedgerTransactionByIdHandler(ILedgerTransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<LedgerTransactionResponse?> Handle(GetLedgerTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var t = await _repository.GetByIdAsync(Guid.Parse(request.Id), Guid.Empty);
        if (t == null) return null;

        return new LedgerTransactionResponse(
            t.Id.ToString(), t.Type, t.Amount, t.Concept, t.Method, t.TransactionRef,
            new TransactionRelatedUsersDto(t.RelatedUsers.PayerName, t.RelatedUsers.StudentName), t.CreatedAt);
    }
}