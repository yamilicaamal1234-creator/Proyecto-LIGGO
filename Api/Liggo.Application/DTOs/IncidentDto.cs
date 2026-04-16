namespace Liggo.Application.DTOs
{
    public class IncidentDto
    {
        public string Id { get; set; } = string.Empty;
        public string PlayerId { get; set; } = string.Empty;
        public string PlayerName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Severity { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Closed { get; set; }
        public DateTime Date { get; set; }
    }
}