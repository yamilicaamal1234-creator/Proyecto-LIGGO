namespace App.Application.DTOs;

public class LoginResponseDto
{
    public string Uid { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    
    // Multi-tenancy: a qué escuela entra por defecto y a cuáles tiene acceso
    public string ActiveTenantId { get; set; } = string.Empty; 
    public List<string> Tenants { get; set; } = new();
    
    // Aquí después pondremos el token JWT
    public string Token { get; set; } = string.Empty; 
}