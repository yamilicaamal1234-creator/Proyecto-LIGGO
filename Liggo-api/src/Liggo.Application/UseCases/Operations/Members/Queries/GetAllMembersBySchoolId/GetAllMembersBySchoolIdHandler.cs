using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.Members.Queries.GetMemberById;
using Liggo.Application.UseCases.Operations.Members.Commands.CreateMember;

namespace Liggo.Application.UseCases.Operations.Members.Queries.GetAllMembersBySchoolId;

public class GetAllMembersBySchoolIdHandler : IRequestHandler<GetAllMembersBySchoolIdQuery, IEnumerable<MemberResponse>>
{
    private readonly IMemberRepository _repository;

    public GetAllMembersBySchoolIdHandler(IMemberRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<MemberResponse>> Handle(GetAllMembersBySchoolIdQuery request, CancellationToken cancellationToken)
    {
        var members = await _repository.GetAllBySchoolIdAsync(request.SchoolId, cancellationToken);
        
        if (members == null || !members.Any())
            return Enumerable.Empty<MemberResponse>();

        return members.Select(member => new MemberResponse(
            member.Id, 
            member.Uid,
            new MemberProfileDto(member.Profile.Name, member.Profile.Phone),
            member.Role,
            new MemberWalletDto(member.Wallet.Balance, member.Wallet.Status, member.Wallet.LastUpdated),
            member.DependentsSummary.ToDictionary(k => k.Key, v => new DependentSummaryDto(v.Value.Team, v.Value.Photo))
        ));
    }
}