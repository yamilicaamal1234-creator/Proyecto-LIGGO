using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;

namespace Liggo.Application.UseCases.Operations.SystemUsers.Commands.DeleteSystemUser;

public class DeleteSystemUserHandler : IRequestHandler<DeleteSystemUserCommand, bool>
{
    private readonly ISystemUserRepository _systemUserRepository;

    public DeleteSystemUserHandler(ISystemUserRepository systemUserRepository)
    {
        _systemUserRepository = systemUserRepository;
    }

    public async Task<bool> Handle(DeleteSystemUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _systemUserRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user == null) return false;

        await _systemUserRepository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}