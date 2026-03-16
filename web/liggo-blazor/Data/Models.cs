namespace liggo_blazor.Data;

public class Player
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Avatar { get; set; } = "";
    public string Category { get; set; } = "";
    public string Team { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Email { get; set; } = "";
    public string PaymentStatus { get; set; } = ""; // al_dia, pendiente, vencido
    public string Position { get; set; } = "";
    public int Number { get; set; }
    public string BirthDate { get; set; } = "";
    public string ParentName { get; set; } = "";
    public string ParentPhone { get; set; } = "";
    public PlayerStats Stats { get; set; } = new();
}

public class PlayerStats
{
    public int Matches { get; set; }
    public int Goals { get; set; }
    public int Assists { get; set; }
}

public class Team
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Color { get; set; } = "";
    public int Players { get; set; }
}

public class Match
{
    public string Id { get; set; } = "";
    public string HomeTeam { get; set; } = "";
    public string AwayTeam { get; set; } = "";
    public string Date { get; set; } = "";
    public string Time { get; set; } = "";
    public string Location { get; set; } = "";
    public int? HomeScore { get; set; }
    public int? AwayScore { get; set; }
    public string Category { get; set; } = "";
    public string Status { get; set; } = ""; // programado, en_curso, finalizado
}

public class Incident
{
    public string Id { get; set; } = "";
    public string PlayerId { get; set; } = "";
    public string PlayerName { get; set; } = "";
    public string Type { get; set; } = ""; // lesion, disciplina, administrativa
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Date { get; set; } = "";
    public string Status { get; set; } = ""; // activa, resuelta
}

public class CalendarEvent
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string Date { get; set; } = "";
    public string Type { get; set; } = ""; // partido, entrenamiento, evento
}

public class MonthlyChartData
{
    public string Month { get; set; } = "";
    public int Asistencias { get; set; }
    public int Pagos { get; set; }
}
