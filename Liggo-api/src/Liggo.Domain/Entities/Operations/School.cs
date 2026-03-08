using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Liggo.Domain.Entities.Operations;

public class School : BaseEntity
{
    // Flattened SchoolInfo properties
    public string Name { get; set; } = string.Empty;
    public string Plan { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;

    // Flattened SchoolSettings properties
    public string Currency { get; set; } = "MXN";
    public string Timezone { get; set; } = "America/Mexico_City";
    
    [NotMapped]
    public SchoolInfo Info { get; set; } = new();
    [NotMapped]
    public SchoolSettings Settings { get; set; } = new();

    // Navigation properties
    public ICollection<Member> Members { get; set; } = new List<Member>();
    public ICollection<LedgerTransaction> LedgerTransactions { get; set; } = new List<LedgerTransaction>();
}

// CategoryInfo might need to be a separate entity or serialized
// public class CategoryInfo
// {
//     public string Name { get; set; } = string.Empty;
//     public List<int> Years { get; set; } = new();
// }