using FluentValidation;
using MediatR;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;

namespace Liggo.Application.Functions.Members.Commands
{
    // 1. EL COMANDO (Lo que manda Angular)
    public class UpdateMemberCommand : IRequest<bool>
    {
        public string Id { get; set; } = string.Empty;
        public string? Name { get; set; } 
        public string? Role { get; set; } 
        public string? Phone { get; set; } 
        public string? Address { get; set; } 
    }

    // 2. EL HANDLER (El Puente)
    public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, bool>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateMemberCommandHandler(IMemberRepository memberRepository, ICurrentUserService currentUserService)
        {
            _memberRepository = memberRepository;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            var schoolId = _currentUserService.SchoolId;

            var existingMember = await _memberRepository.GetByIdAsync(schoolId, request.Id, cancellationToken);

            if (existingMember == null)
            {
                throw new Exception("Miembro no encontrado");
            }

            // 1. CREAMOS EL DICCIONARIO (El parche)
            var updates = new Dictionary<string, object>();

            // 2. LLENAMOS EL PARCHE SOLO CON LO QUE ANGULAR MANDÓ
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                // Usamos la ruta exacta del JSON de Firebase
                // Asumiendo que guardas el nombre en el Profile como acordamos
                updates.Add("profile.fullName", request.Name); 
                
                // ¡EL PUENTE HACIA MYSQL! 🌉
                // Si mandaron nombre, sincronizamos la tabla Members_Reference [cite: 9, 10, 11]
                await _memberRepository.UpdateExternalNameAsync(schoolId, request.Id, request.Name, cancellationToken);
            }

            if (!string.IsNullOrWhiteSpace(request.Role))
            {
                updates.Add("role", request.Role);
            }

            if (!string.IsNullOrWhiteSpace(request.Phone))
            {
                updates.Add("profile.phone", request.Phone);
            }

            if (!string.IsNullOrWhiteSpace(request.Address))
            {
                updates.Add("profile.address", request.Address);
            }

            // 3. MANDAMOS EL PARCHE A FIREBASE (Solo si hay cambios)
            if (updates.Count > 0)
            {
                // Solo mandamos el ID y el parche. ¡Ya no hacemos GetByIdAsync!
                await _memberRepository.UpdatePartialAsync(schoolId, request.Id, updates, cancellationToken);
            }

            return true;
        }
    }
}