using App.Application.DTOs;
using App.Domain.Interfaces;
using MediatR;

namespace App.Application.Features.Auth;

public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponseDto>
{
    private readonly IIdentityService _identityService;

    // Inyectamos la interfaz del Dominio (¡No sabemos si es Firebase, solo sabemos que funciona!)
    public LoginQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<LoginResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        // 1. Buscar al usuario usando el contrato
        var user = await _identityService.AuthenticateUserAsync(request.Email, request.Password);

        // 2. Validar que exista y esté activo
        if (user == null || user.Auth.AccountStatus != "active")
        {
            // En una app real, usaríamos un sistema de errores o Result Pattern, 
            // pero para empezar, lanzamos una excepción clara.
            throw new UnauthorizedAccessException("Credenciales inválidas o cuenta inactiva.");
        }

        // 3. Mapear la entidad al DTO
        return new LoginResponseDto
        {
            Uid = user.Uid,
            Email = user.Auth.Email,
            FullName = user.GlobalProfile.FullName,
            ActiveTenantId = user.ActiveTenantId,
            Tenants = user.Tenants,
            Token = "jwt_token_generado_aqui" 
        };
    }
}