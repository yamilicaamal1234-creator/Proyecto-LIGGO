using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Exceptions;
using Liggo.Application.Interfaces.Operations;

namespace Liggo.Application.UseCases.Operations.Matches.Commands.DeleteMatch
{
    public class DeleteMatchHandler : IRequestHandler<DeleteMatchCommand, Unit>
    {
        private readonly IMatchRepository _matchRepository;

        public DeleteMatchHandler(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<Unit> Handle(DeleteMatchCommand request, CancellationToken cancellationToken)
        {
            var match = await _matchRepository.GetByIdAsync(request.Id, request.AdminId);
            if (match == null)
            {
                throw new NotFoundException($"Match with ID {request.Id} not found.");
            }

            await _matchRepository.DeleteAsync(request.Id, request.AdminId);
            
            return Unit.Value;
        }
    }
}
