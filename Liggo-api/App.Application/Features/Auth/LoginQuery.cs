using App.Application.DTOs;
using MediatR;

namespace App.Application.Features.Auth;

// IRequest le dice a MediatR: "Cuando ejecutes esto, prométeme devolver un LoginResponseDto"
public class LoginQuery : IRequest<LoginResponseDto>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}