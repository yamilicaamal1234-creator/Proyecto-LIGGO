using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Liggo.Application.UseCases.Billing.Plans.Commands.CreatePlan;
using Liggo.Application.UseCases.Billing.Plans.Queries.GetPlanById;

namespace Liggo.Api.Controllers.Billing;

[ApiController]
[Route("api/billing/[controller]")]
public class PlansController : ControllerBase
{
    private readonly ISender _sender;

    public PlansController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlanCommand command)
    {
        var planId = await _sender.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = planId }, new { Id = planId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetPlanByIdQuery(id);
        var plan = await _sender.Send(query);

        if (plan == null)
            return NotFound(new { message = $"No se encontró el Plan con ID {id}" });

        return Ok(plan);
    }
}