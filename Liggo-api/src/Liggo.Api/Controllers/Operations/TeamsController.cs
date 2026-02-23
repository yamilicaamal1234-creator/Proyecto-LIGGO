using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Liggo.Application.UseCases.Operations.Teams.Commands.CreateTeam;
using Liggo.Application.UseCases.Operations.Teams.Queries.GetTeamById;

namespace Liggo.Api.Controllers.Operations;

[ApiController]
[Route("api/operations/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly ISender _sender;

    public TeamsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTeamCommand command)
    {
        var teamId = await _sender.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = teamId }, new { Id = teamId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetTeamByIdQuery(id);
        var team = await _sender.Send(query);

        if (team == null)
            return NotFound(new { message = $"No se encontró el equipo con ID {id}" });

        return Ok(team);
    }
}