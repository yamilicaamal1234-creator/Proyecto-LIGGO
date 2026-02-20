namespace App.Domain.Entities.NoSql;

public class Player
{
    public string Id { get; set; } = string.Empty;
    public PlayerInfo Info { get; set; } = new();
    public string Status { get; set; } = "active";
    public string Team { get; set; } = string.Empty; 
    public List<string> Parents { get; set; } = new(); 
    public PlayerStats Stats { get; set; } = new();
}

public class PlayerInfo
{
    public string Name { get; set; } = string.Empty;
    public string Dob { get; set; } = string.Empty; 
    public string Gender { get; set; } = string.Empty;
    public string PhotoUrl { get; set; } = string.Empty;
}

public class PlayerStats
{
    public int Matches { get; set; }
    public int Goals { get; set; }
    public int Minutes { get; set; }
    public int YellowCards { get; set; }
}