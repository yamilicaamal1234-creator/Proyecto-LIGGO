using System;
using MediatR;

namespace Liggo.Application.UseCases.Operations.Players.Commands.UpdatePlayer
{
    public record UpdatePlayerCommand(
        Guid Id,
        Guid AdminId,
        string FullName,
        DateTime DateOfBirth,
        string AssignedTeam,
        string GuardianName,
        string GuardianPhone,
        string Relationship) : IRequest<Unit>;
}