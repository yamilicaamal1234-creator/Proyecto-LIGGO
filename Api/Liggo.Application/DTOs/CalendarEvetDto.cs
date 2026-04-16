namespace Liggo.Application.DTOs
{
    public class CalendarEventDto
    {
        public string Id { get; set; } = string.Empty;
        public string EventType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime DateStart { get; set; } = DateTime.UtcNow;
        public DateTime DateEnd { get; set; } = DateTime.UtcNow;
        public string LocationName { get; set; } = string.Empty;
        public string AttendanceStatus { get; set; } = string.Empty;
        public int? Goals { get; set; }
        public int? Rating { get; set; }
        public string? Reason { get; set; } = string.Empty;
        public double Lat { get; set; }
        public double Lng { get; set; }
        public Dictionary<string, AttendanceEntryDto> Attendance { get; set; } = new();
    }

    public class AttendanceEntryDto
    {
        public string Status { get; set; } = string.Empty;
        public int? Goals { get; set; }
        public int? Rating { get; set; }
        public string? Reason { get; set; } = string.Empty;
    }
}