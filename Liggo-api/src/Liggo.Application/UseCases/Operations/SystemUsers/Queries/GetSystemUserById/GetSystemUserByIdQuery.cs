using MediatR;
using Liggo.Application.UseCases.Operations.SystemUsers.Queries.Responses;

namespace Liggo.Application.UseCases.Operations.SystemUsers.Queries.GetSystemUserById;

public record GetSystemUserByIdQuery(string Id) : IRequest<SystemUserResponse?>;