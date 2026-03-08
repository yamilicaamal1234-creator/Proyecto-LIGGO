using System;
using System.Collections.Generic;
using MediatR;
using Liggo.Domain.Enums;

namespace Liggo.Application.UseCases.Operations.Attendances.Commands.SaveAttendance
{
    public record AttendanceRecord(Guid PlayerId, AttendanceStatus Status);

    public record SaveAttendanceCommand(
        Guid AdminId,
        Guid MatchId,
        IEnumerable<AttendanceRecord> Attendances) : IRequest<Unit>;
}
