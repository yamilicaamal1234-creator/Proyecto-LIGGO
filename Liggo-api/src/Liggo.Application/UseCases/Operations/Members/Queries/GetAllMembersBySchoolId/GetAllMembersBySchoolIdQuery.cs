using System.Collections.Generic;
using MediatR;
using Liggo.Application.UseCases.Operations.Members.Queries.GetMemberById;

namespace Liggo.Application.UseCases.Operations.Members.Queries.GetAllMembersBySchoolId;

public record GetAllMembersBySchoolIdQuery(string SchoolId) : IRequest<IEnumerable<MemberResponse>>;