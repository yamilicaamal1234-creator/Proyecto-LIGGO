using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;

namespace Liggo.Application.UseCases.Operations.Attendances.Commands.SaveAttendance
{
    public class SaveAttendanceHandler : IRequestHandler<SaveAttendanceCommand, Unit>
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public SaveAttendanceHandler(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public async Task<Unit> Handle(SaveAttendanceCommand request, CancellationToken cancellationToken)
        {
            var attendanceEntities = request.Attendances.Select(att => new Attendance
            {
                AdminId = request.AdminId,
                MatchId = request.MatchId,
                PlayerId = att.PlayerId,
                Status = att.Status
            });

            await _attendanceRepository.AddOrUpdateAttendanceAsync(attendanceEntities);

            return Unit.Value;
        }
    }
}
