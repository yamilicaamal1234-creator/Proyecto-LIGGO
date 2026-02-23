namespace Liggo.Domain.Entities.Operations;

public class Incident
{
    public string Id { get; set; } = string.Empty;
    public string Type { get; set; } = "injury"; // "injury", "discipline"
    public string Severity { get; set; } = "low"; // "low", "medium", "high"
    public IncidentContext Context { get; set; } = new();
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "open"; // "open", "closed"
}

public class IncidentContext
{
    public string Student { get; set; } = string.Empty;
    public string Event { get; set; } = string.Empty; // Ej. "Vs Tigres"
}