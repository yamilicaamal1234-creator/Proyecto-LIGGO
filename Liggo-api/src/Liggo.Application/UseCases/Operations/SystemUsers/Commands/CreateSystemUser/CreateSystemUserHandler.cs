using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;

namespace Liggo.Application.UseCases.Operations.SystemUsers.Commands.CreateSystemUser;

public class CreateSystemUserHandler : IRequestHandler<CreateSystemUserCommand, string>
{
    private readonly ISystemUserRepository _systemUserRepository;

    public CreateSystemUserHandler(ISystemUserRepository systemUserRepository)
    {
        _systemUserRepository = systemUserRepository;
    }

    public async Task<string> Handle(CreateSystemUserCommand request, CancellationToken cancellationToken)
    {
        var user = new SystemUser
        {
            Id = request.Id,
            ActiveTenantId = request.ActiveTenantId,
            Tenants = request.Tenants ?? new System.Collections.Generic.List<string>(),
            CreatedAt = request.CreatedAt == default ? DateTime.UtcNow : request.CreatedAt,
            Auth = new AuthData
            {
                Email = request.Auth.Email,
                Provider = request.Auth.Provider,
                EmailVerified = request.Auth.EmailVerified,
                AccountStatus = string.IsNullOrWhiteSpace(request.Auth.AccountStatus) ? "active" : request.Auth.AccountStatus,
                LastLogin = request.Auth.LastLogin
            },
            GlobalProfile = new GlobalProfile
            {
                FullName = request.GlobalProfile.FullName,
                Phone = request.GlobalProfile.Phone,
                PhotoUrl = request.GlobalProfile.PhotoUrl
            }
        };

        await _systemUserRepository.AddAsync(user, cancellationToken);

        return user.Id;
    }
}