using System;
using MediatR;
using Liggo.Domain.Enums;

namespace Liggo.Application.UseCases.Operations.Matches.Commands.CreateMatch
{
    public record CreateMatchCommand(
        Guid AdminId,
        string LocalTeam,
        string VisitingTeam,
        DateTime DateTime,
        string Location,
        MatchCategory Category) : IRequest<Guid>;
}
