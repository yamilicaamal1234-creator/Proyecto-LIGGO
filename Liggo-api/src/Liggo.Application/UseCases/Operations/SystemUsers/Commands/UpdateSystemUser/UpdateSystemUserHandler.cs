using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;

namespace Liggo.Application.UseCases.Operations.SystemUsers.Commands.UpdateSystemUser;

public class UpdateSystemUserHandler : IRequestHandler<UpdateSystemUserCommand, bool>
{
    private readonly ISystemUserRepository _systemUserRepository;

    public UpdateSystemUserHandler(ISystemUserRepository systemUserRepository)
    {
        _systemUserRepository = systemUserRepository;
    }

    public async Task<bool> Handle(UpdateSystemUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _systemUserRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user == null) return false;

        user.ActiveTenantId = request.ActiveTenantId;
        user.Tenants = request.Tenants ?? new System.Collections.Generic.List<string>();
        
        user.Auth.Email = request.Auth.Email;
        user.Auth.Provider = request.Auth.Provider;
        user.Auth.EmailVerified = request.Auth.EmailVerified;
        user.Auth.AccountStatus = request.Auth.AccountStatus;
        user.Auth.LastLogin = request.Auth.LastLogin;

        user.GlobalProfile.FullName = request.GlobalProfile.FullName;
        user.GlobalProfile.Phone = request.GlobalProfile.Phone;
        user.GlobalProfile.PhotoUrl = request.GlobalProfile.PhotoUrl;

        await _systemUserRepository.UpdateAsync(user, cancellationToken);
        
        return true;
    }
}