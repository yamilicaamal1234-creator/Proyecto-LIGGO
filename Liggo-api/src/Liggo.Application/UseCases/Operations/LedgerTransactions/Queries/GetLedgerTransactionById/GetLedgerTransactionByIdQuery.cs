using MediatR;
using Liggo.Application.UseCases.Operations.LedgerTransactions.Queries.Responses;

namespace Liggo.Application.UseCases.Operations.LedgerTransactions.Queries.GetLedgerTransactionById;

public record GetLedgerTransactionByIdQuery(string Id) : IRequest<LedgerTransactionResponse?>;