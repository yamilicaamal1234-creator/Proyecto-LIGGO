using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Liggo.Application.UseCases.Billing.Tenants.Commands.CreateTenant;
using Liggo.Application.UseCases.Billing.Tenants.Queries.GetTenantById;
// Asegúrate de importar los namespaces correctos de tus Commands y Queries

namespace Liggo.Api.Controllers.Billing;

[ApiController]
[Route("api/billing/[controller]")]
public class TenantsController : ControllerBase
{
    private readonly ISender _sender;

    // Inyectamos MediatR, ¡nada de repositorios!
    public TenantsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTenantCommand command)
    {
        // MediatR busca el Handler, ejecuta el ValidationBehavior, guarda en MySQL y regresa el ID
        var tenantId = await _sender.Send(command);
        
        // Devolvemos un 201 Created con la ruta para consultar el nuevo recurso
        return CreatedAtAction(nameof(GetById), new { id = tenantId }, new { Id = tenantId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetTenantByIdQuery(id);
        var tenant = await _sender.Send(query);

        if (tenant == null)
            return NotFound(new { message = $"No se encontró el Tenant con ID {id}" });

        return Ok(tenant);
    }
}