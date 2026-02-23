using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Liggo.Application.UseCases.Operations.SystemUsers.Commands.CreateSystemUser;
using Liggo.Application.UseCases.Operations.SystemUsers.Queries.GetSystemUserById;

namespace Liggo.Api.Controllers.Operations;

[ApiController]
[Route("api/operations/[controller]")]
public class SystemUsersController : ControllerBase
{
    private readonly ISender _sender;

    public SystemUsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSystemUserCommand command)
    {
        var userId = await _sender.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = userId }, new { Id = userId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetSystemUserByIdQuery(id);
        var user = await _sender.Send(query);

        if (user == null)
            return NotFound(new { message = $"No se encontró el usuario del sistema con ID {id}" });

        return Ok(user);
    }
}