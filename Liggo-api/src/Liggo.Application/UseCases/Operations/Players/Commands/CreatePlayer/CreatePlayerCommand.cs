using System;
using MediatR;

namespace Liggo.Application.UseCases.Operations.Players.Commands.CreatePlayer
{
    public record CreatePlayerCommand(
        Guid AdminId,
        string FullName,
        DateTime DateOfBirth,
        string AssignedTeam,
        string GuardianName,
        string GuardianPhone,
        string Relationship) : IRequest<Guid>;
}