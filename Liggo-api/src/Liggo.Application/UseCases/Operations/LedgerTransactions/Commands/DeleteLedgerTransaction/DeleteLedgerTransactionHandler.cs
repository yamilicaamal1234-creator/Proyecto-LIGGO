using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;

namespace Liggo.Application.UseCases.Operations.LedgerTransactions.Commands.DeleteLedgerTransaction;

public class DeleteLedgerTransactionHandler : IRequestHandler<DeleteLedgerTransactionCommand, bool>
{
    private readonly ILedgerTransactionRepository _repository;

    public DeleteLedgerTransactionHandler(ILedgerTransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteLedgerTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (transaction == null) return false;

        await _repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}