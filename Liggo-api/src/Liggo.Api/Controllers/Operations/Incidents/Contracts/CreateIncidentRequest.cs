namespace Liggo.Api.Controllers.Operations.Incidents.Contracts;

public class IncidentContextRequest
{
    public string Student { get; set; } = string.Empty;
    public string Event { get; set; } = string.Empty;

    public IncidentContextRequest() { }
    public IncidentContextRequest(string student, string @event) { Student = student; Event = @event; }
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

public record UpdateIncidentRequest(
    string Type,
    string Severity,
    IncidentContextRequest Context,
    string Description,
    string Status);