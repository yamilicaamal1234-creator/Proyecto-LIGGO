using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Liggo.Api.Controllers.Operations.Players.Contracts;
using Liggo.Application.UseCases.Operations.Players.Commands.CreatePlayer;
using Liggo.Application.UseCases.Operations.Players.Commands.DeletePlayer;
using Liggo.Application.UseCases.Operations.Players.Commands.UpdatePlayer;
using Liggo.Application.UseCases.Operations.Players.Queries.GetAllPlayers;
using Liggo.Application.UseCases.Operations.Players.Queries.GetPlayerById;

namespace Liggo.Api.Controllers.Operations
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize] - Bypassed for Blazor MVP
    public class PlayersController : ControllerBase
    {
        private readonly ISender _mediator;

        public PlayersController(ISender mediator)
        {
            _mediator = mediator;
        }

        private Guid GetAdminId()
        {
            // Bypassed for MVP: Return a fixed Guid so unauthenticated Blazor calls can save data
            return Guid.Parse("11111111-1111-1111-1111-111111111111");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var adminId = GetAdminId();
            if (adminId == Guid.Empty) return Unauthorized();

            var query = new GetAllPlayersQuery(adminId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var adminId = GetAdminId();
            if (adminId == Guid.Empty) return Unauthorized();

            var query = new GetPlayerByIdQuery(id, adminId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePlayerRequest request)
        {
            var adminId = GetAdminId();
            if (adminId == Guid.Empty) return Unauthorized();

            var command = new CreatePlayerCommand(
                adminId,
                request.FullName,
                request.DateOfBirth,
                request.AssignedTeam,
                request.GuardianName,
                request.GuardianPhone,
                request.Relationship);
            
            var playerId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = playerId }, new { id = playerId });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePlayerRequest request)
        {
            var adminId = GetAdminId();
            if (adminId == Guid.Empty) return Unauthorized();

            var command = new UpdatePlayerCommand(
                id,
                adminId,
                request.FullName,
                request.DateOfBirth,
                request.AssignedTeam,
                request.GuardianName,
                request.GuardianPhone,
                request.Relationship);

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var adminId = GetAdminId();
            if (adminId == Guid.Empty) return Unauthorized();

            var command = new DeletePlayerCommand(id, adminId);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}