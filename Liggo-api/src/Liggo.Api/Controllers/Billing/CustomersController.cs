using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Liggo.Application.UseCases.Billing.Customers.Commands.CreateCustomer;
using Liggo.Application.UseCases.Billing.Customers.Queries.GetCustomerById;
using Liggo.Application.UseCases.Billing.Customers.Queries.GetCustomersByTenantId;

namespace Liggo.Api.Controllers.Billing;

[ApiController]
[Route("api/billing/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ISender _sender;

    public CustomersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
    {
        var customerId = await _sender.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = customerId }, new { Id = customerId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetCustomerByIdQuery(id);
        var customer = await _sender.Send(query);

        if (customer == null)
            return NotFound(new { message = $"No se encontró el Customer con ID {id}" });

        return Ok(customer);
    }

    [HttpGet("tenant/{tenantId}")]
    public async Task<IActionResult> GetByTenantId(int tenantId)
    {
        // Esta lógica llamará a tu repositorio filtrando por tenantId
        // Por simplicidad, asumo que crearemos un Query específico de MediatR para esto
        var customers = await _sender.Send(new GetCustomersByTenantIdQuery(tenantId));
        return Ok(customers);
    }
}