using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.Attendances.Dtos;

namespace Liggo.Application.UseCases.Operations.Attendances.Queries.GetAttendances
{
    public class GetAttendancesHandler : IRequestHandler<GetAttendancesQuery, IEnumerable<AttendanceDto>>
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public GetAttendancesHandler(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public async Task<IEnumerable<AttendanceDto>> Handle(GetAttendancesQuery request, CancellationToken cancellationToken)
        {
            var attendances = await _attendanceRepository.GetAttendancesByMatchIdAsync(request.MatchId, request.AdminId);

            return attendances.Select(att => new AttendanceDto
            {
                PlayerId = att.PlayerId,
                PlayerName = att.Player.FullName,
                Status = att.Status
            });
        }
    }
}
