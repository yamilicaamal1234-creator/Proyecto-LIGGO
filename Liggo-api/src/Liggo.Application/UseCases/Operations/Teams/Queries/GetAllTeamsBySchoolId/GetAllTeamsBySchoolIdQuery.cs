using System.Collections.Generic;
using MediatR;
using Liggo.Application.UseCases.Operations.Teams.Queries.Responses;

namespace Liggo.Application.UseCases.Operations.Teams.Queries.GetAllTeamsBySchoolId;

public record GetAllTeamsBySchoolIdQuery(string SchoolId) : IRequest<IEnumerable<TeamResponse>>;