using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Liggo.Application.UseCases.Operations.Schools.Commands.CreateSchool;
using Liggo.Application.UseCases.Operations.Schools.Queries.GetSchoolById;

namespace Liggo.Api.Controllers.Operations;

[ApiController]
[Route("api/operations/[controller]")]
public class SchoolsController : ControllerBase
{
    private readonly ISender _sender;

    public SchoolsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSchoolCommand command)
    {
        var schoolId = await _sender.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = schoolId }, new { Id = schoolId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetSchoolByIdQuery(id);
        var school = await _sender.Send(query);

        if (school == null)
            return NotFound(new { message = $"No se encontró la escuela con ID {id}" });

        return Ok(school);
    }
}