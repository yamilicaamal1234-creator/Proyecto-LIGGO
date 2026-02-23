using System.Collections.Generic;
using MediatR;
using Liggo.Application.UseCases.Operations.LedgerTransactions.Queries.GetLedgerTransactionById;

namespace Liggo.Application.UseCases.Operations.LedgerTransactions.Queries.GetAllLedgerTransactionsByMemberId;

public record GetAllLedgerTransactionsByMemberIdQuery(string MemberId) : IRequest<IEnumerable<LedgerTransactionResponse>>;