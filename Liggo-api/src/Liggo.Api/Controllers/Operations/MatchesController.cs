using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Liggo.Api.Controllers.Operations.Matches.Contracts;
using Liggo.Application.UseCases.Operations.Matches.Commands.CreateMatch;
using Liggo.Application.UseCases.Operations.Matches.Commands.DeleteMatch;
using Liggo.Application.UseCases.Operations.Matches.Queries.GetAllMatches;
using Liggo.Application.UseCases.Operations.Matches.Queries.GetMatchById;
using Liggo.Application.UseCases.Operations.Matches.Commands.UpdateMatch;

namespace Liggo.Api.Controllers.Operations
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize] - Bypassed for Blazor MVP
    public class MatchesController : ControllerBase
    {
        private readonly ISender _mediator;

        public MatchesController(ISender mediator)
        {
            _mediator = mediator;
        }

        private Guid GetAdminId()
        {
            return Guid.Parse("11111111-1111-1111-1111-111111111111");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var adminId = GetAdminId();
            if (adminId == Guid.Empty) return Unauthorized();

            var query = new GetAllMatchesQuery(adminId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var adminId = GetAdminId();
            if (adminId == Guid.Empty) return Unauthorized();

            var query = new GetMatchByIdQuery(id, adminId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMatchRequest request)
        {
            var adminId = GetAdminId();
            if (adminId == Guid.Empty) return Unauthorized();

            var command = new CreateMatchCommand(
                adminId,
                request.LocalTeam,
                request.VisitingTeam,
                request.DateTime,
                request.Location,
                request.Category);
            
            var matchId = await _mediator.Send(command);
            return Ok(new { id = matchId });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMatchRequest request)
        {
            var adminId = GetAdminId();
            if (adminId == Guid.Empty) return Unauthorized();

            var command = new UpdateMatchCommand(
                id,
                adminId,
                request.LocalTeam,
                request.VisitingTeam,
                request.DateTime,
                request.Location,
                request.Category);

            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var adminId = GetAdminId();
            if (adminId == Guid.Empty) return Unauthorized();

            var command = new DeleteMatchCommand(id, adminId);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
