namespace liggo_blazor.Data;

public static class MockData
{
    private static string AvatarUrl(string seed) => $"https://api.dicebear.com/7.x/avataaars/svg?seed={seed}";

    public static List<Player> Players = new()
    {
        new Player { Id = "1", Name = "Santiago Martínez", Avatar = AvatarUrl("santiago"), Category = "Sub-13", Team = "Halcones FC", Phone = "+52 55 1234 5678", Email = "smartinez@email.com", PaymentStatus = "al_dia", Position = "Delantero", Number = 9, BirthDate = "2012-03-15", ParentName = "Carlos Martínez", ParentPhone = "+52 55 9876 5432", Stats = new PlayerStats { Matches = 24, Goals = 12, Assists = 8 } },
        new Player { Id = "2", Name = "Mateo López", Avatar = AvatarUrl("mateo"), Category = "Sub-11", Team = "Águilas Doradas", Phone = "+52 55 2345 6789", Email = "mlopez@email.com", PaymentStatus = "pendiente", Position = "Mediocampista", Number = 10, BirthDate = "2014-07-22", ParentName = "Ana López", ParentPhone = "+52 55 8765 4321", Stats = new PlayerStats { Matches = 18, Goals = 5, Assists = 11 } },
        new Player { Id = "3", Name = "Valentina García", Avatar = AvatarUrl("valentina"), Category = "Sub-15", Team = "Halcones FC", Phone = "+52 55 3456 7890", Email = "vgarcia@email.com", PaymentStatus = "al_dia", Position = "Defensa", Number = 4, BirthDate = "2010-11-08", ParentName = "Roberto García", ParentPhone = "+52 55 7654 3210", Stats = new PlayerStats { Matches = 30, Goals = 2, Assists = 5 } },
        new Player { Id = "4", Name = "Sebastián Hernández", Avatar = AvatarUrl("sebastian"), Category = "Sub-9", Team = "Leones Azules", Phone = "+52 55 4567 8901", Email = "shernandez@email.com", PaymentStatus = "vencido", Position = "Portero", Number = 1, BirthDate = "2016-01-30", ParentName = "María Hernández", ParentPhone = "+52 55 6543 2109", Stats = new PlayerStats { Matches = 10, Goals = 0, Assists = 1 } },
        new Player { Id = "5", Name = "Isabella Torres", Avatar = AvatarUrl("isabella"), Category = "Sub-13", Team = "Águilas Doradas", Phone = "+52 55 5678 9012", Email = "itorres@email.com", PaymentStatus = "al_dia", Position = "Mediocampista", Number = 8, BirthDate = "2012-09-14", ParentName = "Luis Torres", ParentPhone = "+52 55 5432 1098", Stats = new PlayerStats { Matches = 22, Goals = 7, Assists = 9 } },
        new Player { Id = "6", Name = "Diego Ramírez", Avatar = AvatarUrl("diego"), Category = "Sub-17", Team = "Halcones FC", Phone = "+52 55 6789 0123", Email = "dramirez@email.com", PaymentStatus = "al_dia", Position = "Delantero", Number = 11, BirthDate = "2008-05-20", ParentName = "Patricia Ramírez", ParentPhone = "+52 55 4321 0987", Stats = new PlayerStats { Matches = 35, Goals = 20, Assists = 12 } },
        new Player { Id = "7", Name = "Camila Flores", Avatar = AvatarUrl("camila"), Category = "Sub-11", Team = "Leones Azules", Phone = "+52 55 7890 1234", Email = "cflores@email.com", PaymentStatus = "pendiente", Position = "Defensa", Number = 3, BirthDate = "2014-12-03", ParentName = "Jorge Flores", ParentPhone = "+52 55 3210 9876", Stats = new PlayerStats { Matches = 15, Goals = 1, Assists = 3 } },
        new Player { Id = "8", Name = "Emiliano Cruz", Avatar = AvatarUrl("emiliano"), Category = "Sub-15", Team = "Águilas Doradas", Phone = "+52 55 8901 2345", Email = "ecruz@email.com", PaymentStatus = "al_dia", Position = "Mediocampista", Number = 6, BirthDate = "2010-08-17", ParentName = "Sandra Cruz", ParentPhone = "+52 55 2109 8765", Stats = new PlayerStats { Matches = 28, Goals = 4, Assists = 14 } },
        new Player { Id = "9", Name = "Luciana Morales", Avatar = AvatarUrl("luciana"), Category = "Sub-9", Team = "Halcones FC", Phone = "+52 55 9012 3456", Email = "lmorales@email.com", PaymentStatus = "vencido", Position = "Delantero", Number = 7, BirthDate = "2016-04-25", ParentName = "Fernando Morales", ParentPhone = "+52 55 1098 7654", Stats = new PlayerStats { Matches = 8, Goals = 3, Assists = 2 } },
        new Player { Id = "10", Name = "Andrés Vargas", Avatar = AvatarUrl("andres"), Category = "Sub-17", Team = "Leones Azules", Phone = "+52 55 0123 4567", Email = "avargas@email.com", PaymentStatus = "al_dia", Position = "Portero", Number = 1, BirthDate = "2008-02-11", ParentName = "Gloria Vargas", ParentPhone = "+52 55 0987 6543", Stats = new PlayerStats { Matches = 32, Goals = 0, Assists = 2 } },
    };

    public static List<Team> Teams = new()
    {
        new Team { Id = "1", Name = "Halcones FC", Color = "#00C853", Players = 28 },
        new Team { Id = "2", Name = "Águilas Doradas", Color = "#FFB100", Players = 24 },
        new Team { Id = "3", Name = "Leones Azules", Color = "#007BFF", Players = 22 },
    };

    public static List<Match> Matches = new()
    {
        new Match { Id = "1", HomeTeam = "Halcones FC", AwayTeam = "Águilas Doradas", Date = "2026-02-15", Time = "10:00", Location = "Campo Norte", HomeScore = 3, AwayScore = 1, Category = "Sub-13", Status = "finalizado" },
        new Match { Id = "2", HomeTeam = "Leones Azules", AwayTeam = "Halcones FC", Date = "2026-02-18", Time = "11:00", Location = "Campo Sur", HomeScore = 2, AwayScore = 2, Category = "Sub-15", Status = "finalizado" },
        new Match { Id = "3", HomeTeam = "Águilas Doradas", AwayTeam = "Leones Azules", Date = "2026-02-22", Time = "09:00", Location = "Campo Central", HomeScore = null, AwayScore = null, Category = "Sub-11", Status = "programado" },
        new Match { Id = "4", HomeTeam = "Halcones FC", AwayTeam = "Leones Azules", Date = "2026-02-25", Time = "10:30", Location = "Campo Norte", HomeScore = null, AwayScore = null, Category = "Sub-17", Status = "programado" },
        new Match { Id = "5", HomeTeam = "Águilas Doradas", AwayTeam = "Halcones FC", Date = "2026-03-01", Time = "09:30", Location = "Campo Sur", HomeScore = null, AwayScore = null, Category = "Sub-9", Status = "programado" },
    };

    public static List<Incident> Incidents = new()
    {
        new Incident { Id = "1", PlayerId = "1", PlayerName = "Santiago Martínez", Type = "lesion", Title = "Esguince de tobillo", Description = "Esguince grado 1 durante entrenamiento. Reposo 2 semanas.", Date = "2026-01-20", Status = "resuelta" },
        new Incident { Id = "2", PlayerId = "6", PlayerName = "Diego Ramírez", Type = "disciplina", Title = "Tarjeta roja", Description = "Expulsado por conducta antideportiva en partido vs Leones.", Date = "2026-02-05", Status = "activa" },
        new Incident { Id = "3", PlayerId = "4", PlayerName = "Sebastián Hernández", Type = "administrativa", Title = "Documentación pendiente", Description = "Falta acta de nacimiento para registro en liga.", Date = "2026-01-15", Status = "activa" },
        new Incident { Id = "4", PlayerId = "3", PlayerName = "Valentina García", Type = "lesion", Title = "Contractura muscular", Description = "Contractura en cuádriceps derecho. Fisioterapia.", Date = "2026-02-08", Status = "activa" },
        new Incident { Id = "5", PlayerId = "8", PlayerName = "Emiliano Cruz", Type = "disciplina", Title = "Falta a entrenamientos", Description = "3 faltas consecutivas sin justificación.", Date = "2026-02-01", Status = "resuelta" },
    };

    public static List<CalendarEvent> UpcomingEvents = new()
    {
        new CalendarEvent { Id = "1", Title = "Águilas vs Leones (Sub-11)", Date = "2026-02-22", Type = "partido" },
        new CalendarEvent { Id = "2", Title = "Entrenamiento Sub-13", Date = "2026-02-23", Type = "entrenamiento" },
        new CalendarEvent { Id = "3", Title = "Halcones vs Leones (Sub-17)", Date = "2026-02-25", Type = "partido" },
        new CalendarEvent { Id = "4", Title = "Junta de padres", Date = "2026-02-27", Type = "evento" },
        new CalendarEvent { Id = "5", Title = "Águilas vs Halcones (Sub-9)", Date = "2026-03-01", Type = "partido" },
    };

    public static List<MonthlyChartData> MonthlyChartData = new()
    {
        new MonthlyChartData { Month = "Sep", Asistencias = 85, Pagos = 92 },
        new MonthlyChartData { Month = "Oct", Asistencias = 88, Pagos = 87 },
        new MonthlyChartData { Month = "Nov", Asistencias = 82, Pagos = 90 },
        new MonthlyChartData { Month = "Dic", Asistencias = 75, Pagos = 78 },
        new MonthlyChartData { Month = "Ene", Asistencias = 90, Pagos = 85 },
        new MonthlyChartData { Month = "Feb", Asistencias = 87, Pagos = 88 },
    };
}
