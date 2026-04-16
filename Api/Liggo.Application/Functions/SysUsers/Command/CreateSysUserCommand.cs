using FluentValidation;
using MediatR;
using Liggo.Domain.Entities.Documents;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;

namespace Liggo.Application.Functions.SysUsers.Commands 
{
    public class CreateSysUserCommand : IRequest<string>
    {
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
    }

    public class CreateSysUserCommandValidator : AbstractValidator<CreateSysUserCommand>
    {
        public CreateSysUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.FullName).NotEmpty();
        }
    }

    public class CreateSysUserCommandHandler : IRequestHandler<CreateSysUserCommand, string>
    {
        private readonly ISysUserRepository _sysUserRepository;
        private readonly ICurrentUserService _currentUser; 

        public CreateSysUserCommandHandler(ISysUserRepository sysUserRepository, ICurrentUserService currentUser)
        {
            _sysUserRepository = sysUserRepository;
            _currentUser = currentUser;
        }

        public async Task<string> Handle(CreateSysUserCommand request, CancellationToken cancellationToken)
        {
            // A. SEGURIDAD: Sacamos el UID real y seguro del token de Firebase Auth 
            string secureFirebaseUid = _currentUser.UserId;

            // B. VALIDACIÓN EXTRA: Revisamos si ya existe para no planchar sus datos
            var existingUser = await _sysUserRepository.GetByIdAsync(secureFirebaseUid, cancellationToken);
            if (existingUser != null)
            {
                throw new Exception("El usuario ya está registrado en el sistema.");
            }

            // C. ARMAMOS LA ENTIDAD
            var newSysUser = new Liggo.Domain.Entities.Documents.SysUser
            {
                Id = secureFirebaseUid, 
                Email = request.Email,
                FullName = request.FullName,
                ActiveTenantId = string.Empty, 
                Tenants = new List<string>()
            };

            // D. GUARDAMOS EN FIREBASE
            await _sysUserRepository.AddAsync(newSysUser, cancellationToken);

            // E. Retornamos el UID generado
            return newSysUser.Id;
        }
    }
}