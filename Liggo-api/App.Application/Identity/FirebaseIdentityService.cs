using App.Domain.Entities.NoSql;
using App.Domain.Interfaces;

namespace App.Infrastructure.Identity;

// Esta clase HEREDA de la interfaz, lo que la obliga a tener los mismos métodos
public class FirebaseIdentityService : IIdentityService
{
    // Aquí es donde inyectarías el SDK de FirebaseAdmin real en el futuro
    public FirebaseIdentityService() { }

    public async Task<SystemUser?> AuthenticateUserAsync(string email, string password)
    {
        // En producción, aquí harías la llamada real a Firebase Auth.
        // Por ahora, devolveremos datos estáticos simulando tu archivo JSON para que puedas probar.
        await Task.Delay(100); // Simulamos el tiempo de red

        if(email == "padre@gmail.com") // Basado en tu documento de diseño
        {
            return new SystemUser
            {
                Uid = "auth_uid_123",
                ActiveTenantId = "school_rayados",
                Tenants = new List<string> { "school_rayados", "school_tigres" },
                Auth = new AuthInfo { Email = email, AccountStatus = "active" },
                GlobalProfile = new GlobalProfile { FullName = "Juan Pérez" }
            };
        }
        
        return null; // Credenciales incorrectas
    }

    public Task<SystemUser?> GetUserByIdAsync(string uid)
    {
        throw new NotImplementedException();
    }
}