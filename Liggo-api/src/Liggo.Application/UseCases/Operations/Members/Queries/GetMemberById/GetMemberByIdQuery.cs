using MediatR;

namespace Liggo.Application.UseCases.Operations.Members.Queries.GetMemberById;

public record GetMemberByIdQuery(string Id) : IRequest<MemberResponse?>;