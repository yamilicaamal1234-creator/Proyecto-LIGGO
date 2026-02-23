using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.Members.Commands.CreateMember;

namespace Liggo.Application.UseCases.Operations.Members.Queries.GetMemberById;

public class GetMemberByIdHandler : IRequestHandler<GetMemberByIdQuery, MemberResponse?>
{
    private readonly IMemberRepository _repository;

    public GetMemberByIdHandler(IMemberRepository repository)
    {
        _repository = repository;
    }

    public async Task<MemberResponse?> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        var member = await _repository.GetByIdAsync(request.Id, cancellationToken);
        
        if (member == null) return null;

        return new MemberResponse(
            member.Id, 
            member.Uid,
            new MemberProfileDto(member.Profile.Name, member.Profile.Phone),
            member.Role,
            new MemberWalletDto(member.Wallet.Balance, member.Wallet.Status, member.Wallet.LastUpdated),
            member.DependentsSummary.ToDictionary(k => k.Key, v => new DependentSummaryDto(v.Value.Team, v.Value.Photo))
        );
    }
}