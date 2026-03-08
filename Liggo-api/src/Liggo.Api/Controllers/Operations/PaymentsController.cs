using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Liggo.Api.Controllers.Operations.Payments.Contracts;
using Liggo.Application.UseCases.Operations.Payments.Commands.CreatePayment;
using Liggo.Application.UseCases.Operations.Payments.Queries.GetAllPayments;
using Liggo.Application.UseCases.Operations.Payments.Commands.UpdatePayment;
using Liggo.Application.UseCases.Operations.Payments.Commands.DeletePayment;

namespace Liggo.Api.Controllers.Operations
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize] - Bypassed for Blazor MVP
    public class PaymentsController : ControllerBase
    {
        private readonly ISender _mediator;

        public PaymentsController(ISender mediator)
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

            var query = new GetAllPaymentsQuery(adminId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentRequest request)
        {
            var adminId = GetAdminId();
            if (adminId == Guid.Empty) return Unauthorized();

            var command = new CreatePaymentCommand(
                adminId,
                request.PlayerId,
                request.Concept,
                request.Amount,
                request.Date,
                request.Status);
            
            var paymentId = await _mediator.Send(command);
            return Ok(new { id = paymentId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePaymentRequest request)
        {
            var adminId = GetAdminId();
            if (adminId == Guid.Empty) return Unauthorized();

            var command = new UpdatePaymentCommand(
                id,
                adminId,
                request.PlayerId,
                request.Concept,
                request.Amount,
                request.Date,
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

            var command = new DeletePaymentCommand(id, adminId);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
