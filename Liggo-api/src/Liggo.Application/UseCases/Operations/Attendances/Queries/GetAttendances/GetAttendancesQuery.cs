using System;
using System.Collections.Generic;
using MediatR;
using Liggo.Application.UseCases.Operations.Attendances.Dtos;

namespace Liggo.Application.UseCases.Operations.Attendances.Queries.GetAttendances
{
    public record GetAttendancesQuery(Guid MatchId, Guid AdminId) : IRequest<IEnumerable<AttendanceDto>>;
}
