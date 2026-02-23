using MediatR;
using Liggo.Application.UseCases.Operations.SystemUsers.Queries.Responses;

namespace Liggo.Application.UseCases.Operations.SystemUsers.Queries.GetSystemUserByEmail;

public record GetSystemUserByEmailQuery(string Email) : IRequest<SystemUserResponse?>;