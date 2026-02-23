using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;

namespace Liggo.Application.UseCases.Operations.Members.Commands.UpdateMember;

public class UpdateMemberHandler : IRequestHandler<UpdateMemberCommand, bool>
{
    private readonly IMemberRepository _memberRepository;

    public UpdateMemberHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<bool> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(request.Id, cancellationToken);
        if (member == null) return false;

        // Actualizamos las propiedades
        member.Uid = request.Uid;
        member.Role = request.Role;
        
        member.Profile.Name = request.Profile.Name;
        member.Profile.Phone = request.Profile.Phone;

        member.Wallet.Balance = request.Wallet.Balance;
        member.Wallet.Status = request.Wallet.Status;
        member.Wallet.LastUpdated = request.Wallet.LastUpdated;

        // Reconstruimos el diccionario
        member.DependentsSummary = request.DependentsSummary?.ToDictionary(
            k => k.Key,
            v => new Liggo.Domain.Entities.Operations.DependentSummary
            {
                Team = v.Value.Team,
                Photo = v.Value.Photo
            }) ?? new Dictionary<string, Liggo.Domain.Entities.Operations.DependentSummary>();

        await _memberRepository.UpdateAsync(member, cancellationToken);
        
        return true;
    }
}