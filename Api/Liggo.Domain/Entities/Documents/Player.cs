namespace Liggo.Domain.Entities.Documents
{
    public class Player
    {
        public string Id { get; set; } = string.Empty;
        public PlayerInfo Info { get; set; } = new();
        public string TeamId { get; set; } = string.Empty;
        public string MemberId { get; set; } = string.Empty;
        public StatsSumary Stats { get; set; } = new();
    }

    public class PlayerInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Dob { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Weight { get; set; } = string.Empty;
        public string Height { get; set; } = string.Empty;
    }
    public class StatsSumary
    {
        public int Matches { get; set; }
        public int Goals { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
        public float AvgRating { get; set; }
    }
}