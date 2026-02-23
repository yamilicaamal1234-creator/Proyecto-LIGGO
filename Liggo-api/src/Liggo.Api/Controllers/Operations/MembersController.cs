using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Liggo.Application.UseCases.Operations.Members.Commands.CreateMember;
using Liggo.Application.UseCases.Operations.Members.Queries.GetMemberById;

namespace Liggo.Api.Controllers.Operations;

[ApiController]
[Route("api/operations/[controller]")]
public class MembersController : ControllerBase
{
    private readonly ISender _sender;

    public MembersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMemberCommand command)
    {
        var memberId = await _sender.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = memberId }, new { Id = memberId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetMemberByIdQuery(id);
        var member = await _sender.Send(query);

        if (member == null)
            return NotFound(new { message = $"No se encontró el miembro con ID {id}" });

        return Ok(member);
    }
}