using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Liggo.Application.UseCases.Operations.Players.Commands.CreatePlayer;
using Liggo.Application.UseCases.Operations.Players.Queries.GetPlayerById;

namespace Liggo.Api.Controllers.Operations;

[ApiController]
[Route("api/operations/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly ISender _sender;

    public PlayersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlayerCommand command)
    {
        var playerId = await _sender.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = playerId }, new { Id = playerId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetPlayerByIdQuery(id);
        var player = await _sender.Send(query);

        if (player == null)
            return NotFound(new { message = $"No se encontró el jugador con ID {id}" });

        return Ok(player);
    }
}