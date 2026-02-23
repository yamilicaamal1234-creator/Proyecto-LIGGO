using System.Collections.Generic;
using MediatR;
using Liggo.Application.UseCases.Operations.Incidents.Queries.GetIncidentById; // Reusamos el DTO de respuesta

namespace Liggo.Application.UseCases.Operations.Incidents.Queries.GetAllIncidentsBySchoolId;

public record GetAllIncidentsBySchoolIdQuery(string SchoolId) : IRequest<IEnumerable<IncidentResponse>>;