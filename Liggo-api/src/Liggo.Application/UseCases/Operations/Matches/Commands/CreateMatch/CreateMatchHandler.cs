using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;

namespace Liggo.Application.UseCases.Operations.Matches.Commands.CreateMatch
{
    public class CreateMatchHandler : IRequestHandler<CreateMatchCommand, Guid>
    {
        private readonly IMatchRepository _matchRepository;

        public CreateMatchHandler(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<Guid> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
        {
            var match = new Match
            {
                Id = Guid.NewGuid(),
                AdminId = request.AdminId,
                LocalTeam = request.LocalTeam,
                VisitingTeam = request.VisitingTeam,
                DateTime = request.DateTime,
                Location = request.Location,
                Category = request.Category
            };

            await _matchRepository.AddAsync(match);

            return match.Id;
        }
    }
}
