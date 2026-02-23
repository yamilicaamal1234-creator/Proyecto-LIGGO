using System;
using MediatR;
using Liggo.Application.UseCases.Operations.LedgerTransactions.Commands.CreateLedgerTransaction;

namespace Liggo.Application.UseCases.Operations.LedgerTransactions.Queries.GetLedgerTransactionById;

public record LedgerTransactionResponse(
    string Id,
    string Type,
    decimal Amount,
    string Concept,
    string Method,
    string TransactionRef,
    TransactionRelatedUsersDto RelatedUsers,
    DateTime CreatedAt);

public record GetLedgerTransactionByIdQuery(string Id) : IRequest<LedgerTransactionResponse?>;

// Validator
public class GetLedgerTransactionByIdValidator : FluentValidation.AbstractValidator<GetLedgerTransactionByIdQuery>
{
    public GetLedgerTransactionByIdValidator() => RuleFor(x => x.Id).NotEmpty();
}

// Handler
public class GetLedgerTransactionByIdHandler : IRequestHandler<GetLedgerTransactionByIdQuery, LedgerTransactionResponse?>
{
    private readonly ILedgerTransactionRepository _repository;
    public GetLedgerTransactionByIdHandler(ILedgerTransactionRepository repository) => _repository = repository;

    public async Task<LedgerTransactionResponse?> Handle(GetLedgerTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var t = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (t == null) return null;

        return new LedgerTransactionResponse(
            t.Id, t.Type, t.Amount, t.Concept, t.Method, t.TransactionRef,
            new TransactionRelatedUsersDto(t.RelatedUsers.PayerName, t.RelatedUsers.StudentName), t.CreatedAt);
    }
}