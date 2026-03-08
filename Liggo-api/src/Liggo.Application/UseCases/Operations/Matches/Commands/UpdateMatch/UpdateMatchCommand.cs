using MediatR;

namespace Liggo.Application.UseCases.Operations.Matches.Commands.UpdateMatch;

public record UpdateMatchCommand(
    Guid Id,
    Guid AdminId,
    string LocalTeam,
    string VisitingTeam,
    DateTime DateTime,
    string Location,
    string Category) : IRequest<bool>;