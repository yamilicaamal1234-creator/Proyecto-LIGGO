using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Liggo.Domain.Enums;

namespace Liggo.Api.Controllers.Operations.Attendances.Contracts
{
    public class AttendanceRecordRequest
    {
        [Required]
        public Guid PlayerId { get; set; }
        [Required]
        public AttendanceStatus Status { get; set; }
    }

    public class SaveAttendanceRequest
    {
        [Required]
        public Guid MatchId { get; set; }

        [Required]
        [MinLength(1)]
        public IEnumerable<AttendanceRecordRequest> Attendances { get; set; } = new List<AttendanceRecordRequest>();
    }
}
