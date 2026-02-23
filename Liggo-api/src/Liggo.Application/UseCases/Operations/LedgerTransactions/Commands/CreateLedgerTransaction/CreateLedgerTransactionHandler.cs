using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;

namespace Liggo.Application.UseCases.Operations.LedgerTransactions.Commands.CreateLedgerTransaction;

public class CreateLedgerTransactionHandler : IRequestHandler<CreateLedgerTransactionCommand, string>
{
    private readonly ILedgerTransactionRepository _repository;

    public CreateLedgerTransactionHandler(ILedgerTransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> Handle(CreateLedgerTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = new LedgerTransaction
        {
            Id = Guid.NewGuid().ToString(), // ID para Firebase
            Type = request.Type,
            Amount = request.Amount,
            Concept = request.Type == "charge" ? request.Concept : string.Empty,
            Method = request.Type == "payment" ? request.Method : string.Empty,
            TransactionRef = request.Type == "payment" ? request.TransactionRef : string.Empty,
            CreatedAt = request.CreatedAt == default ? DateTime.UtcNow : request.CreatedAt,
            RelatedUsers = new TransactionRelatedUsers
            {
                PayerName = request.RelatedUsers.PayerName,
                StudentName = request.RelatedUsers.StudentName
            }
        };

        await _repository.AddAsync(transaction, cancellationToken);

        return transaction.Id;
    }
}