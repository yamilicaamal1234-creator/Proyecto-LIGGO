using MediatR;
using Liggo.Application.UseCases.Operations.Teams.Queries.Responses;

namespace Liggo.Application.UseCases.Operations.Teams.Queries.GetTeamById;

public record GetTeamByIdQuery(string Id) : IRequest<TeamResponse?>;