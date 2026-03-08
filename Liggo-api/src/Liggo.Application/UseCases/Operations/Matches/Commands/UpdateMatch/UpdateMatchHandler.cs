using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Enums;

namespace Liggo.Application.UseCases.Operations.Matches.Commands.UpdateMatch;

public class UpdateMatchHandler : IRequestHandler<UpdateMatchCommand, bool>
{
    private readonly IMatchRepository _matchRepository;

    public UpdateMatchHandler(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public async Task<bool> Handle(UpdateMatchCommand request, CancellationToken cancellationToken)
    {
        var match = await _matchRepository.GetByIdAsync(request.Id, request.AdminId);

        if (match == null) return false;

        match.LocalTeam = request.LocalTeam;
        match.VisitingTeam = request.VisitingTeam;
        match.DateTime = request.DateTime;
        match.Location = request.Location;
        match.Category = Enum.Parse<MatchCategory>(request.Category);

        await _matchRepository.UpdateAsync(match);

        return true;
    }
}