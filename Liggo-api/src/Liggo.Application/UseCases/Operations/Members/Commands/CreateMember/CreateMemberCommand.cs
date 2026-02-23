using System;
using System.Collections.Generic;
using MediatR;

namespace Liggo.Application.UseCases.Operations.Members.Commands.CreateMember;

// DTOs para mantener el Command limpio y estructurado
public record MemberProfileDto(string Name, string Phone);
public record MemberWalletDto(decimal Balance, string Status, DateTime LastUpdated);
public record DependentSummaryDto(string Team, string Photo);

public record CreateMemberCommand(
    string Uid,
    MemberProfileDto Profile,
    string Role,
    MemberWalletDto Wallet,
    Dictionary<string, DependentSummaryDto> DependentsSummary) : IRequest<string>;