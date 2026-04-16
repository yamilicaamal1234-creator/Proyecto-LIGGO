using MediatR;
using Liggo.Application.DTOs;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;

namespace Liggo.Appplication.Functions.SysUser.Queries
{
    public class GetSysUserByIdQuery : IRequest<SysUserDto>
    {
        //public string FirebaseUid { get; set; } = string.Empty;
    }

    public class GetSysUserByIdQueryHandler : IRequestHandler<GetSysUserByIdQuery, SysUserDto>
    {
        private readonly ISysUserRepository _sysUserRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetSysUserByIdQueryHandler(ISysUserRepository sysUserRepository, ICurrentUserService currentUserService)
        {
            _sysUserRepository = sysUserRepository;
            _currentUserService = currentUserService;
        }

        public async Task<SysUserDto> Handle(GetSysUserByIdQuery request, CancellationToken cancellationToken)
        {
            var SysUserId = _currentUserService.UserId;

            if (SysUserId == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            var sysUser = await _sysUserRepository.GetByIdAsync(SysUserId, cancellationToken);

            if (sysUser == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            var dto = new SysUserDto
            {
                Id = sysUser.Id,
                Email = sysUser.Email,
                FullName = sysUser.FullName,
                ActiveTenantId = sysUser.ActiveTenantId,
                Tenants = sysUser.Tenants
            };

            return dto;
        }
    }
}