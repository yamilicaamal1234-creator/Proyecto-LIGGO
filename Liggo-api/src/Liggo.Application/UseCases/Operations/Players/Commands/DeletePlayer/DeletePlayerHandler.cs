using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;

namespace Liggo.Application.UseCases.Operations.Players.Commands.DeletePlayer;

public class DeletePlayerHandler : IRequestHandler<DeletePlayerCommand, bool>
{
    private readonly IPlayerRepository _playerRepository;

    public DeletePlayerHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<bool> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = await _playerRepository.GetByIdAsync(request.Id, cancellationToken);
        if (player == null) return false;

        await _playerRepository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}