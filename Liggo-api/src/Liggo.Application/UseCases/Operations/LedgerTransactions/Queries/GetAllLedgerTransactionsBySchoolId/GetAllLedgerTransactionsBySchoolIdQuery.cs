using System.Collections.Generic;
using MediatR;
using Liggo.Application.UseCases.Operations.LedgerTransactions.Queries.Responses;

namespace Liggo.Application.UseCases.Operations.LedgerTransactions.Queries.GetAllLedgerTransactionsBySchoolId;

public record GetAllLedgerTransactionsBySchoolIdQuery(string SchoolId) : IRequest<IEnumerable<LedgerTransactionResponse>>;