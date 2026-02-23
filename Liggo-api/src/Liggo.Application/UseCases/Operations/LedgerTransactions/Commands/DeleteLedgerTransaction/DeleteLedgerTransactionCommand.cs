using MediatR;

public record DeleteLedgerTransactionCommand(string Id) : IRequest<bool>;