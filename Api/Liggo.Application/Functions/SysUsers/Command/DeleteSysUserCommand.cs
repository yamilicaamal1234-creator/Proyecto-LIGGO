using MediatR;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;

namespace Liggo.Application.Functions.SysUser.Command
{
    public class DeleteSysUserCommand : IRequest<bool>
    {
    }

    public class DeleteSysUserCommandHandler : IRequestHandler<DeleteSysUserCommand, bool>
    {
        private readonly ISysUserRepository _sysUserRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteSysUserCommandHandler(ISysUserRepository sysUserRepository, ICurrentUserService currentUserService)
        {
            _sysUserRepository = sysUserRepository;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(DeleteSysUserCommand request, CancellationToken cancellationToken)
        {
            string secureFirebaseUid = _currentUserService.UserId;

            var existingSysUser = await _sysUserRepository.GetByIdAsync(secureFirebaseUid, cancellationToken);

            if (existingSysUser == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            // Aquí podrías implementar una lógica de "borrado suave" si no quieres eliminar completamente el documento
            // Por ejemplo, podrías agregar un campo "IsDeleted" y marcarlo como true en lugar de eliminarlo

            // Si decides eliminarlo completamente, necesitarás agregar un método DeleteAsync en tu repositorio
            // await _sysUserRepository.DeleteAsync(secureFirebaseUid, cancellationToken);

            return true;
        }
    }
}