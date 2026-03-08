using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;

namespace Liggo.Application.UseCases.Operations.LedgerTransactions.Commands.UpdateLedgerTransaction;

public class UpdateLedgerTransactionHandler : IRequestHandler<UpdateLedgerTransactionCommand, bool>
{
    private readonly ILedgerTransactionRepository _repository;

    public UpdateLedgerTransactionHandler(ILedgerTransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateLedgerTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _repository.GetByIdAsync(Guid.Parse(request.Id), Guid.Empty);
        if (transaction == null) return false;

        transaction.Type = request.Type;
        transaction.Amount = request.Amount;
        
        // Limpiamos los campos que no correspondan al tipo actual
        transaction.Concept = request.Type == "charge" ? request.Concept : string.Empty;
        transaction.Method = request.Type == "payment" ? request.Method : string.Empty;
        transaction.TransactionRef = request.Type == "payment" ? request.TransactionRef : string.Empty;
        
        transaction.RelatedUsers = new Liggo.Domain.Entities.Operations.TransactionRelatedUsers
        {
            PayerName = request.RelatedUsers.PayerName,
            StudentName = request.RelatedUsers.StudentName
        };

        await _repository.UpdateAsync(transaction);
        return true;
    }
}