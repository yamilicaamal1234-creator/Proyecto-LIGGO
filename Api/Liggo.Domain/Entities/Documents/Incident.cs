namespace Liggo.Domain.Entities.Documents
{
    public class Incident
    {
        public string Id { get; set; } = string.Empty;
        public string PlayerId { get; set; } = string.Empty;
        public string IncidentType { get; set; } = string.Empty;
        public string Severeity { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Closed { get; set; } = false;
    }
}