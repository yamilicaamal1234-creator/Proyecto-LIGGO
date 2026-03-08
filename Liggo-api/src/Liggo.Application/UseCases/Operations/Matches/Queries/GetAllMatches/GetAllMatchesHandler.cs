using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.Matches.Dtos;

namespace Liggo.Application.UseCases.Operations.Matches.Queries.GetAllMatches
{
    public class GetAllMatchesHandler : IRequestHandler<GetAllMatchesQuery, IEnumerable<MatchDto>>
    {
        private readonly IMatchRepository _matchRepository;

        public GetAllMatchesHandler(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<IEnumerable<MatchDto>> Handle(GetAllMatchesQuery request, CancellationToken cancellationToken)
        {
            var matches = await _matchRepository.GetAllByAdminIdAsync(request.AdminId);

            return matches.Select(match => new MatchDto
            {
                Id = match.Id,
                LocalTeam = match.LocalTeam,
                VisitingTeam = match.VisitingTeam,
                DateTime = match.DateTime,
                Location = match.Location,
                Category = match.Category.ToString()
            });
        }
    }
}
