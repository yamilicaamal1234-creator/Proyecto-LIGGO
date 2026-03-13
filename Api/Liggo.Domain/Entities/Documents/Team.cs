namespace Liggo.Domain.Entities.Documents
{
    public class Team
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string CoachId { get; set; } = string.Empty;
        public List<string> Roster { get; set; } = new();
    }
}