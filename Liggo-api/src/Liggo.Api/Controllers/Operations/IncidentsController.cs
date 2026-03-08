using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Liggo.Api.Controllers.Operations.Incidents.Contracts;
using Liggo.Application.UseCases.Operations.Incidents.Commands.CreateIncident;
using Liggo.Application.UseCases.Operations.Incidents.Queries.GetAllIncidentsByAdminId;
using Liggo.Application.UseCases.Operations.Incidents.Queries.GetIncidentById;
using Liggo.Application.UseCases.Operations.Incidents.Commands.UpdateIncident;
using Liggo.Application.UseCases.Operations.Incidents.Commands.DeleteIncident;

namespace Liggo.Api.Controllers.Operations
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize] - Bypassed for Blazor MVP
    public class IncidentsController : ControllerBase
    {
        private readonly ISender _mediator;

        public IncidentsController(ISender mediator)
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

            var query = new GetAllIncidentsByAdminIdQuery(adminId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var adminId = GetAdminId();
            if (adminId == Guid.Empty) return Unauthorized();

            var query = new GetIncidentByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateIncidentRequest request)
        {
            var adminId = GetAdminId();
            if (adminId == Guid.Empty) return Unauthorized();

            var command = new CreateIncidentCommand(
                adminId,
                request.PlayerId,
                request.Type,
                request.Severity,
                new Liggo.Application.UseCases.Operations.Incidents.Commands.CreateIncident.IncidentContextDto(request.Context.Student, request.Context.Event),
                request.Description,
                request.Status);

            var incidentId = await _mediator.Send(command);
            return Ok(new { id = incidentId });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateIncidentRequest request)
        {
            var adminId = GetAdminId();
            if (adminId == Guid.Empty) return Unauthorized();

            var command = new UpdateIncidentCommand(
                id.ToString(),
                request.Type,
                request.Severity,
                new Liggo.Application.UseCases.Operations.Incidents.Commands.CreateIncident.IncidentContextDto(request.Context.Student, request.Context.Event),
                request.Description,
                request.Status);

            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var adminId = GetAdminId();
            if (adminId == Guid.Empty) return Unauthorized();

            var command = new DeleteIncidentCommand(id.ToString());
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}