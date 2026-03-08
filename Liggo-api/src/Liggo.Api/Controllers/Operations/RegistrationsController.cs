using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Liggo.Api.Controllers.Operations.Registrations.Contracts;
using Liggo.Application.UseCases.Operations.Registrations.Commands.CreateRegistration;
using Liggo.Application.UseCases.Operations.Registrations.Queries.GetAllRegistrations;
using Liggo.Application.UseCases.Operations.Registrations.Commands.UpdateRegistration;
using Liggo.Application.UseCases.Operations.Registrations.Commands.DeleteRegistration;

namespace Liggo.Api.Controllers.Operations
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize] - Bypassed for Blazor MVP
    public class RegistrationsController : ControllerBase
    {
        private readonly ISender _mediator;

        public RegistrationsController(ISender mediator)
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

            var query = new GetAllRegistrationsQuery(adminId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRegistrationRequest request)
        {
            var adminId = GetAdminId();
            if (adminId == Guid.Empty) return Unauthorized();

            var command = new CreateRegistrationCommand(
                adminId,
                request.PlayerId,
                request.Plan,
                request.StartDate,
                request.EndDate,
                request.Amount,
                request.Status);
            
            var registrationId = await _mediator.Send(command);
            // In a real scenario, we might return CreatedAtAction, but this is simpler for now.
            return Ok(new { id = registrationId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRegistrationRequest request)
        {
            var adminId = GetAdminId();
            if (adminId == Guid.Empty) return Unauthorized();

            var command = new UpdateRegistrationCommand(
                id,
                adminId,
                request.Plan,
                request.StartDate,
                request.EndDate,
                request.Amount,
                request.Status);

            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var adminId = GetAdminId();
            if (adminId == Guid.Empty) return Unauthorized();

            var command = new DeleteRegistrationCommand(id, adminId);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
