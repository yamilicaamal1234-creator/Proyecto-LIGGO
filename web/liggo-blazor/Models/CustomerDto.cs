namespace liggo_blazor.Models;

public class CustomerDto
{
    public int Id { get; set; }
    public string ExternalId { get; set; } = string.Empty;
    public string BusinessName { get; set; } = string.Empty;
    public string TaxId { get; set; } = string.Empty;
    public string AdminEmail { get; set; } = string.Empty;
}
