namespace Liggo.Domain.Entities.Documents
{
    public class CalendarEvent
    {
        public string Id { get; set; } = string.Empty;
        public string EventType { get; set; } = string.Empty;
        public EventMetaData MetaData { get; set; } = new();
        public string Status { get; set; } = string.Empty;
        public Dictionary<string, AttendenceEntry> AttendenceMap { get; set; } = new();
    }
    
    public class EventMetaData
    {
        public string Title { get; set; } = string.Empty;
        public DateTime DateStart { get; set; } = DateTime.UtcNow;
        public DateTime DateEnd { get; set; } = DateTime.UtcNow;
        public string LocationName { get; set; } = string.Empty;
        public GeoCoordinates Geo { get; set; } = new();
    }

    public class AttendenceEntry
    {
        public string Status { get; set; } = string.Empty;
        public int? Goals { get; set; }
        public int? Rating { get; set; }
        public string? Reason { get; set; } = string.Empty;
    }

    public class GeoCoordinates 
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}