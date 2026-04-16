using FluentValidation;
using MediatR;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;

namespace Liggo.Application.Functions.SysUsers.Command
{
    public class UpdateSysUserCommand : IRequest<bool>
    {
        public string? FullName { get; set; } = string.Empty;
        public string? ActiveTenantId { get; set; } = string.Empty;
    }

    public class UpdateSysUserCommandHandler : IRequestHandler<UpdateSysUserCommand, bool>
    {
        private readonly ISysUserRepository _sysUserRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMemberRepository _memberRepository;
        private readonly IMemberReferenceRepository _memberReferenceRepository;

        public UpdateSysUserCommandHandler(ISysUserRepository sysUserRepository, ICurrentUserService currentUserService, IMemberRepository memberRepository, IMemberReferenceRepository memberReferenceRepository)
        {
            _sysUserRepository = sysUserRepository;
            _currentUserService = currentUserService;
            _memberRepository = memberRepository;
            _memberReferenceRepository = memberReferenceRepository;
        }

        public async Task<bool> Handle(UpdateSysUserCommand request, CancellationToken cancellationToken)
        {
            string secureFirebaseUid = _currentUserService.UserId;
            string secureSchoolId = _currentUserService.SchoolId;

            var updates = new Dictionary<string, object>();

            if (request.FullName != null)
            {
                updates.Add("FullName", request.FullName);

                await _memberReferenceRepository.UpdateExternalNameAsync(secureFirebaseUid, request.FullName, cancellationToken);
            }
            if (request.ActiveTenantId != null)
            {
                updates.Add("ActiveTenantId", request.ActiveTenantId);
            }

            if (updates.Count > 0)
            {
                await _sysUserRepository.UpdatePartialAsync(secureFirebaseUid, updates, cancellationToken);
            }
            return true;
        }
    }
}