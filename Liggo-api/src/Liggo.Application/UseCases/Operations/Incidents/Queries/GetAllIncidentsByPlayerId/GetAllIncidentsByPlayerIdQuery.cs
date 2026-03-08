using System.Collections.Generic;
using MediatR;
using Liggo.Application.UseCases.Operations.Incidents.Queries.GetIncidentById;

namespace Liggo.Application.UseCases.Operations.Incidents.Queries.GetAllIncidentsByPlayerId;

public record GetAllIncidentsByPlayerIdQuery(Guid PlayerId) : IRequest<IEnumerable<IncidentResponse>>;