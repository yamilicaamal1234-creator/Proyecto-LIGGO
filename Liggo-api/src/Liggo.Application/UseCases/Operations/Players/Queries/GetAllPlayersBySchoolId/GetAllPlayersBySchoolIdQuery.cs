using System.Collections.Generic;
using MediatR;
using Liggo.Application.UseCases.Operations.Players.Queries.Responses;

namespace Liggo.Application.UseCases.Operations.Players.Queries.GetAllPlayersBySchoolId;

public record GetAllPlayersBySchoolIdQuery(string SchoolId) : IRequest<IEnumerable<PlayerResponse>>;