using System;
using Liggo.Application.UseCases.Operations.LedgerTransactions.Commands.CreateLedgerTransaction;

namespace Liggo.Application.UseCases.Operations.LedgerTransactions.Queries.Responses;

public record LedgerTransactionResponse(
    string Id,
    string Type,
    decimal Amount,
    string Concept,
    string Method,
    string TransactionRef,
    TransactionRelatedUsersDto RelatedUsers,
    DateTime CreatedAt);