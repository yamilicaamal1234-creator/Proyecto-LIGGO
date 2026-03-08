using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Exceptions;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.Matches.Dtos;

namespace Liggo.Application.UseCases.Operations.Matches.Queries.GetMatchById
{
    public class GetMatchByIdHandler : IRequestHandler<GetMatchByIdQuery, MatchDto>
    {
        private readonly IMatchRepository _matchRepository;

        public GetMatchByIdHandler(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<MatchDto> Handle(GetMatchByIdQuery request, CancellationToken cancellationToken)
        {
            var match = await _matchRepository.GetByIdAsync(request.Id, request.AdminId);

            if (match == null)
            {
                throw new NotFoundException($"Match with ID {request.Id} not found.");
            }

            return new MatchDto
            {
                Id = match.Id,
                LocalTeam = match.LocalTeam,
                VisitingTeam = match.VisitingTeam,
                DateTime = match.DateTime,
                Location = match.Location,
                Category = match.Category.ToString()
            };
        }
    }
}
