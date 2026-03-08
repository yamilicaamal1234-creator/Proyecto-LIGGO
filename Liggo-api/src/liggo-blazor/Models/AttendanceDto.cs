using System;

namespace liggo_blazor.Models
{
    public enum AttendanceStatus
    {
        Present = 41,
        Absent = 42,
        Justified = 43,
        Tardy = 44
    }

    public class AttendanceDto
    {
        public Guid PlayerId { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public AttendanceStatus Status { get; set; }
    }
}
