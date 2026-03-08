using System;
using Liggo.Domain.Enums;

namespace Liggo.Domain.Entities.Operations
{
    public class Attendance : BaseEntity
    {
        public Guid PlayerId { get; set; }
        public Guid MatchId { get; set; }
        public AttendanceStatus Status { get; set; }

        // Navigation properties
        public Player Player { get; set; } = null!;
        public Match Match { get; set; } = null!;
    }
}
