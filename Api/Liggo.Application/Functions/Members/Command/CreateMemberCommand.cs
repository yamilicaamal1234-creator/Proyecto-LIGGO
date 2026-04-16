using FluentValidation;
using MediatR;
using Liggo.Domain.Entities.Documents;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;
using System.Collections.Generic;

namespace Liggo.Application.Functions.Members.Commands
{
    public class CreateMemberCommand : IRequest<string>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

    public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
    {
        public CreateMemberCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Un correo válido es obligatorio para la invitación");
            RuleFor(x => x.Role).NotEmpty().WithMessage("El rol es obligatorio");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("El teléfono es obligatorio");
            RuleFor(x => x.Address).NotEmpty().WithMessage("La dirección es obligatoria");
        }
    }

    public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, string>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ISysUserRepository _sysUserRepository;

        public CreateMemberCommandHandler(IMemberRepository memberRepository, ICurrentUserService currentUserService, ISysUserRepository sysUserRepository)
        {
            _memberRepository = memberRepository;
            _currentUserService = currentUserService;
            _sysUserRepository = sysUserRepository;
        }

        public async Task<string> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            //Buscamos la escuela
            var schoolId = _currentUserService.SchoolId;
            //Generamos un Id para el usuario que todavía no se registra 
            var newMemberId = Guid.NewGuid().ToString();
            var generatedGlobalUid = Guid.NewGuid().ToString();

            var newMember = new Liggo.Domain.Entities.Documents.Member
            {
                Id = newMemberId, 
                Uid = generatedGlobalUid,
                MemberName = request.Name,
                Role = request.Role,
                Profile = new Liggo.Domain.Entities.Documents.MemberProfile 
                {
                    Phone = request.Phone,
                    Address = request.Address,
                    PhotoUrl = "" 
                }
            };

            // 4. Armamos la Cuenta Global (SysUser). Usamos ruta completa.
            var newSysUser = new Liggo.Domain.Entities.Documents.SysUser
            {
                Id = generatedGlobalUid, // Mismo ID exacto que pusimos arriba
                Email = request.Email,
                FullName = request.Name,
                ActiveTenantId = schoolId, 
                Tenants = new List<string> { schoolId } // Le damos acceso a esta escuela
            };

            await _sysUserRepository.AddAsync(newSysUser, cancellationToken);
            await _memberRepository.AddAsync(schoolId, newMember, cancellationToken);

            // Devolvemos el ID local para la tabla de Angular
            return newMemberId;
        }
    }
}   