namespace Liggo.Application.DTOs
{
    public class PlayerDto
    {
        public Guid Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string AgeOrDob { get; set; } = string.Empty;
        public int TotalGoals { get; set; }
        public float AverageRating { get; set; }
    }
}