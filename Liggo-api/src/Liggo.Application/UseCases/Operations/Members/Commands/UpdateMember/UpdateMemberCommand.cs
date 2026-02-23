using System.Collections.Generic;
using MediatR;
using Liggo.Application.UseCases.Operations.Members.Commands.CreateMember; // Reusamos los DTOs

namespace Liggo.Application.UseCases.Operations.Members.Commands.UpdateMember;

public record UpdateMemberCommand(
    string Id,
    string Uid,
    MemberProfileDto Profile,
    string Role,
    MemberWalletDto Wallet,
    Dictionary<string, DependentSummaryDto> DependentsSummary) : IRequest<bool>;