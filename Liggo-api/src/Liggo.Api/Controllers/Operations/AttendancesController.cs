using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Liggo.Api.Controllers.Operations.Attendances.Contracts;
using Liggo.Application.UseCases.Operations.Attendances.Commands.SaveAttendance;
using Liggo.Application.UseCases.Operations.Attendances.Queries.GetAttendances;

namespace Liggo.Api.Controllers.Operations
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize] - Bypassed for Blazor MVP
    public class AttendancesController : ControllerBase
    {
        private readonly ISender _mediator;

        public AttendancesController(ISender mediator)
        {
            _mediator = mediator;
        }

        private Guid GetAdminId()
        {
            return Guid.Parse("11111111-1111-1111-1111-111111111111");
        }

        [HttpGet("{matchId:guid}")]
        public async Task<IActionResult> GetByMatchId(Guid matchId)
        {
            var adminId = GetAdminId();
            if (adminId == Guid.Empty) return Unauthorized();

            var query = new GetAttendancesQuery(matchId, adminId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] SaveAttendanceRequest request)
        {
            var adminId = GetAdminId();
            if (adminId == Guid.Empty) return Unauthorized();

            var command = new SaveAttendanceCommand(
                adminId,
                request.MatchId,
                request.Attendances.Select(a => new Application.UseCases.Operations.Attendances.Commands.SaveAttendance.AttendanceRecord(a.PlayerId, a.Status))
            );
            
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
