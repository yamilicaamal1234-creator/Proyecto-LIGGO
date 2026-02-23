using MediatR;

namespace Liggo.Application.UseCases.Operations.SystemUsers.Commands.DeleteSystemUser;

public record DeleteSystemUserCommand(string Id) : IRequest<bool>;