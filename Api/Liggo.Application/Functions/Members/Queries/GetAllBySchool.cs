using MediatR;
using Liggo.Application.DTOs;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;

namespace Liggo.Application.Functions.Members.Queries
{
    public class GetMembersBySchoolQuery : IRequest<List<MemberDto>>
    {
        // El SchoolId no va aquí por seguridad, lo saca el servidor.
        
        // Pero SÍ permitimos que Angular nos mande un filtro opcional.
        // Si manda null, traemos a todos. Si manda "coach", traemos solo coaches.
        public string? RoleFilter { get; set; } 
    }

    // ==========================================================
    // 2. EL HANDLER (El Trabajador)
    // ==========================================================
    public class GetMembersBySchoolQueryHandler : IRequestHandler<GetMembersBySchoolQuery, List<MemberDto>>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetMembersBySchoolQueryHandler( IMemberRepository memberRepository, ICurrentUserService currentUserService)
        {
            _memberRepository = memberRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<MemberDto>> Handle(GetMembersBySchoolQuery request, CancellationToken cancellationToken)
        {
            // A. SEGURIDAD: Inyectamos la escuela del usuario que hace la petición
            string secureSchoolId = _currentUserService.SchoolId;

            // B. BÚSQUEDA: Llamamos al repositorio. Fíjate cómo le pasamos el RoleFilter que manda Angular
            var members = await _memberRepository.GetAllBySchoolAsync(secureSchoolId, request.RoleFilter, cancellationToken);

            var dtos = members.Select(member => new MemberDto
            {
                Id = member.Id,
                Uid = member.Uid, 
                Name = member.MemberName, 
                Role = member.Role, 
                Phone = member.Profile.Phone,
                Address = member.Profile.Address,
                PhotoUrl = member.Profile.PhotoUrl,
                WalletBalance = member.Wallet.Balance,
                WalletStatus = member.Wallet.Status
            }).ToList();

            return dtos;
        }
    }
}