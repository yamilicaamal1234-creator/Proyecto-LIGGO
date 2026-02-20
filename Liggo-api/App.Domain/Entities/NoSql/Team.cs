namespace App.Domain.Entities.NoSql;

public class Team
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Coach { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public TeamStats StatsTeam { get; set; } = new();
}

public class TeamStats
{
    public int Won { get; set; }
    public int Lost { get; set; }
}