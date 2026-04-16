using MediatR;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;

namespace Liggo.Application.Functions.Members.Commands
{
    public class DeleteMemberCommand : IRequest<bool>
    {
        public string Id { get; set; } = string.Empty;
    }

    public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, bool>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteMemberCommandHandler(IMemberRepository memberRepository, ICurrentUserService currentUserService)
        {
            _memberRepository = memberRepository;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            var schoolId = _currentUserService.SchoolId;

            var existingMember = await _memberRepository.GetByIdAsync(schoolId, request.Id, cancellationToken);

            if (existingMember == null)
            {
                throw new Exception("Miembro no encontrado");
            }

            await _memberRepository.DeleteAsync(schoolId, request.Id, cancellationToken);

            return true;
        }
    }
}