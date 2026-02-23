using System.Collections.Generic;
using MediatR;
using Liggo.Application.UseCases.Operations.LedgerTransactions.Queries.GetLedgerTransactionById; // Reusamos el DTO de respuesta

namespace Liggo.Application.UseCases.Operations.LedgerTransactions.Queries.GetAllLedgerTransactionsBySchoolId;

public record GetAllLedgerTransactionsBySchoolIdQuery(string SchoolId) : IRequest<IEnumerable<LedgerTransactionResponse>>;