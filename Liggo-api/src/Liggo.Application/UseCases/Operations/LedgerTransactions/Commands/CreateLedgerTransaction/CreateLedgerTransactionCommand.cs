using System;
using MediatR;

namespace Liggo.Application.UseCases.Operations.LedgerTransactions.Commands.CreateLedgerTransaction;

// DTO para los usuarios relacionados
public record TransactionRelatedUsersDto(string PayerName, string StudentName);

public record CreateLedgerTransactionCommand(
    string Type,
    decimal Amount,
    string Concept,
    string Method,
    string TransactionRef,
    TransactionRelatedUsersDto RelatedUsers,
    DateTime CreatedAt) : IRequest<string>; // Retorna el ID generado en Firebase