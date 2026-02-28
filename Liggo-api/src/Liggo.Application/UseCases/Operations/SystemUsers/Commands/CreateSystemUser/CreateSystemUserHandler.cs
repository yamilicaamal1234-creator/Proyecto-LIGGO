using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.Interfaces.Billing;
using Liggo.Domain.Entities.Operations;
using Liggo.Domain.Entities.Billing;

namespace Liggo.Application.UseCases.Operations.SystemUsers.Commands.CreateSystemUser;

public class CreateSystemUserHandler : IRequestHandler<CreateSystemUserCommand, string>
{
    private readonly ISystemUserRepository _systemUserRepository;
    private readonly ITenantRepository _tenantRepository;

    public CreateSystemUserHandler(ISystemUserRepository systemUserRepository, ITenantRepository tenantRepository)
    {
        _systemUserRepository = systemUserRepository;
        _tenantRepository = tenantRepository;
    }

    public async Task<string> Handle(CreateSystemUserCommand request, CancellationToken cancellationToken)
    {
        // 1. Crear el Tenant en MySQL primero para obtener su ID autoincremental
        var newTenant = new Tenant
        {
            Name = string.IsNullOrWhiteSpace(request.ActiveTenantId) ? "Mi Escuela" : request.ActiveTenantId,
            ApiKey = Guid.NewGuid().ToString("N") // Generamos una API Key única
        };

        await _tenantRepository.AddAsync(newTenant, cancellationToken);

        // 2. Crear el SystemUser en Firestore vinculándolo al TenantId de MySQL
        var user = new SystemUser
        {
            Id = request.Id,
            ActiveTenantId = newTenant.Id.ToString(), // Guardamos el ID real de MySQL
            Tenants = new System.Collections.Generic.List<string> { newTenant.Id.ToString() },
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