namespace Liggo.Domain.Entities.Operations;

public class CalendarEvent
{
    public string Id { get; set; } = string.Empty;
    public string Type { get; set; } = "match"; // "match", "training"
    public EventMetadata Metadata { get; set; } = new();
    public EventLocation Location { get; set; } = new();
    public string Status { get; set; } = "scheduled"; // "scheduled", "finalized", "canceled"
    public EventScore Score { get; set; } = new();
    
    // Diccionario para registrar asistencia y métricas (Clave: PlayerId/Nombre)
    public Dictionary<string, AttendanceRecord> Attendance { get; set; } = new();
}

public class EventMetadata
{
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}

public class EventLocation
{
    public string Name { get; set; } = string.Empty;
    public List<double> Geo { get; set; } = new(); // [lat, lng]
}

public class EventScore
{
    public int Home { get; set; }
    public int Away { get; set; }
}

public class AttendanceRecord
{
    public string Status { get; set; } = "present"; // "present", "absent"
    public int Minutes { get; set; }
    public int Goals { get; set; }
    public int Rating { get; set; }
    public string Note { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty; // Si Status == "absent"
}