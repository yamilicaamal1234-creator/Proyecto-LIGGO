using System.Collections.Generic;
using MediatR;
using Liggo.Application.UseCases.Operations.Incidents.Queries.GetIncidentById; // Reusamos el DTO de respuesta

namespace Liggo.Application.UseCases.Operations.Incidents.Queries.GetAllIncidentsByAdminId;

public record GetAllIncidentsByAdminIdQuery(Guid AdminId) : IRequest<IEnumerable<IncidentResponse>>;