using System;
using MediatR;
using Liggo.Application.UseCases.Operations.LedgerTransactions.Commands.CreateLedgerTransaction; // Reusamos el DTO

namespace Liggo.Application.UseCases.Operations.LedgerTransactions.Commands.UpdateLedgerTransaction;

public record UpdateLedgerTransactionCommand(
    string Id,
    string Type,
    decimal Amount,
    string Concept,
    string Method,
    string TransactionRef,
    TransactionRelatedUsersDto RelatedUsers) : IRequest<bool>;