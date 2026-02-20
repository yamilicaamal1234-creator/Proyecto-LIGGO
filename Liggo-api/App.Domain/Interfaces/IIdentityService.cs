using App.Domain.Entities.NoSql;

namespace App.Domain.Interfaces;

public interface IIdentityService
{
    // Valida correo y contraseña
    Task<SystemUser?> AuthenticateUserAsync(string email, string password);
    
    // Obtiene el perfil del usuario autenticado
    Task<SystemUser?> GetUserByIdAsync(string uid);
}