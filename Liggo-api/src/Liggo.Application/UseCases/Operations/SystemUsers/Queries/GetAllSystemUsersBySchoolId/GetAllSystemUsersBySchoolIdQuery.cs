using System.Collections.Generic;
using MediatR;
using Liggo.Application.UseCases.Operations.SystemUsers.Queries.Responses;

namespace Liggo.Application.UseCases.Operations.SystemUsers.Queries.GetAllSystemUsersBySchoolId;

public record GetAllSystemUsersBySchoolIdQuery(string SchoolId) : IRequest<IEnumerable<SystemUserResponse>>;