using App.Application.Features.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // Esto creará la ruta web: localhost:xxxx/api/auth
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginQuery request)
    {
        try
        {
            // ¡MediatR hace toda la magia! Busca tu LoginQueryHandler automáticamente
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            // Si el Handler lanza una excepción de credenciales inválidas, respondemos un 401
            return Unauthorized(new { error = ex.Message });
        }
    }
}