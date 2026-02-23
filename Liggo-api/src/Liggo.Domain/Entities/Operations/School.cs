namespace Liggo.Domain.Entities.Operations;

public class School
{
    public string Id { get; set; } = string.Empty; // Ej: "school_rayados" (Mismo que Customer.ExternalId en SQL)
    public SchoolInfo Info { get; set; } = new();
    public SchoolSettings Settings { get; set; } = new();
}

public class SchoolInfo
{
    public string Name { get; set; } = string.Empty;
    public string Plan { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
}

public class SchoolSettings
{
    public string Currency { get; set; } = "MXN";
    public string Timezone { get; set; } = "America/Mexico_City";
    public List<CategoryInfo> Categories { get; set; } = new();
}

public class CategoryInfo
{
    public string Name { get; set; } = string.Empty;
    public List<int> Years { get; set; } = new();
}