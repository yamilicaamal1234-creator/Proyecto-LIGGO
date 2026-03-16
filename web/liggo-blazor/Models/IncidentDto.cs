namespace liggo_blazor.Models;

public class IncidentDto
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public IncidentContextDto Context { get; set; } = new();
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class IncidentContextDto
{
    public string Student { get; set; } = string.Empty;
    public string Event { get; set; } = string.Empty;
}

// Esta es la clase que faltaba y causaba el error de compilación en Blazor
public class IncidentContextRequest
{
    public string Student { get; set; } = string.Empty;
    public string Event { get; set; } = string.Empty;
}
public class ReportStatsDto
{
    public double AverageAttendance { get; set; }
    public double GoalsPerMatch { get; set; }
    public double PaymentsOnTime { get; set; }
    public List<TopPlayerDto> TopPlayers { get; set; } = new();
}

public class TopPlayerDto
{
    public string Name { get; set; } = string.Empty;
    public int Goals { get; set; }
    public int Assists { get; set; }
    public int Matches { get; set; }
    public double Rating { get; set; }
}
public class CreateIncidentRequest
{
    public string PlayerId { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public IncidentContextRequest Context { get; set; } = new();
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}

public class UpdateIncidentRequest
{
    public string Type { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public IncidentContextRequest Context { get; set; } = new();
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}