using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;

namespace Liggo.Application.UseCases.Operations.Members.Commands.CreateMember;

public class CreateMemberHandler : IRequestHandler<CreateMemberCommand, string>
{
    private readonly IMemberRepository _memberRepository;

    public CreateMemberHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<string> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var member = new Member
        {
            Id = Guid.NewGuid().ToString(), // ID principal en Firebase
            Uid = request.Uid,
            Role = request.Role,
            Profile = new MemberProfile
            {
                Name = request.Profile.Name,
                Phone = request.Profile.Phone
            },
            Wallet = new MemberWallet
            {
                Balance = request.Wallet.Balance,
                Status = request.Wallet.Status,
                LastUpdated = request.Wallet.LastUpdated == default ? DateTime.UtcNow : request.Wallet.LastUpdated
            },
            DependentsSummary = request.DependentsSummary?.ToDictionary(
                k => k.Key,
                v => new DependentSummary
                {
                    Team = v.Value.Team,
                    Photo = v.Value.Photo
                }) ?? new Dictionary<string, DependentSummary>()
        };

        await _memberRepository.AddAsync(member, cancellationToken);

        return member.Id;
    }
}