using System.Collections.Generic;
using Liggo.Application.UseCases.Operations.Members.Commands.CreateMember; // Para los DTOs anidados

namespace Liggo.Application.UseCases.Operations.Members.Queries.GetMemberById;

public record MemberResponse(
    string Id,
    string Uid,
    MemberProfileDto Profile,
    string Role,
    MemberWalletDto Wallet,
    Dictionary<string, DependentSummaryDto> DependentsSummary);