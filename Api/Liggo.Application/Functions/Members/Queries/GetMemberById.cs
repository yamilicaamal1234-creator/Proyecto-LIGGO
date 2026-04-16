using MediatR;
using Liggo.Application.DTOs;
using Liggo.Domain.Interfaces;
using Liggo.Application.Interfaces;

namespace Liggo.Application.Functions.Member.Queries
{
    public class GetMemberByIdQuery : IRequest<MemberDto>
    {
        public string MemberId { get; set; } = string.Empty;
    }

    public class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, MemberDto>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetMemberByIdQueryHandler(IMemberRepository memberRepository, ICurrentUserService currentUserService)
        {
            _memberRepository = memberRepository;
            _currentUserService = currentUserService;
        }

        public async Task<MemberDto> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var schoolId = _currentUserService.SchoolId;

            var member = await _memberRepository.GetByIdAsync(schoolId, request.MemberId, cancellationToken);

            if (member == null)
            {
                throw new Exception("Miembro no encontrado");
            }

            var dto = new MemberDto
            {
                Id = member.Id,
                Uid = member.Uid,
                Name = member.MemberName,
                Role = member.Role,
                Phone = member.Profile.Phone,
                Address = member.Profile.Address,
                PhotoUrl = member.Profile.PhotoUrl
            };

            return dto;
        }
    }
}