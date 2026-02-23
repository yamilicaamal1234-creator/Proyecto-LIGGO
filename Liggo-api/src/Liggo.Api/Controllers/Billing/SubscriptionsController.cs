using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Liggo.Application.UseCases.Billing.Subscriptions.Commands.CreateSubscription;
using Liggo.Application.UseCases.Billing.Subscriptions.Queries.GetSubscriptionById;

namespace Liggo.Api.Controllers.Billing;

[ApiController]
[Route("api/billing/[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly ISender _sender;

    public SubscriptionsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSubscriptionCommand command)
    {
        var subscriptionId = await _sender.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = subscriptionId }, new { Id = subscriptionId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetSubscriptionByIdQuery(id);
        var subscription = await _sender.Send(query);

        if (subscription == null)
            return NotFound(new { message = $"No se encontró la Suscripción con ID {id}" });

        return Ok(subscription);
    }
}