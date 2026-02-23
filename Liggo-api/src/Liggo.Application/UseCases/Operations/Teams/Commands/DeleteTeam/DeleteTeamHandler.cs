using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;

namespace Liggo.Application.UseCases.Operations.Teams.Commands.DeleteTeam;

public class DeleteTeamHandler : IRequestHandler<DeleteTeamCommand, bool>
{
    private readonly ITeamRepository _teamRepository;

    public DeleteTeamHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<bool> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await _teamRepository.GetByIdAsync(request.Id, cancellationToken);
        if (team == null) return false;

        await _teamRepository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}