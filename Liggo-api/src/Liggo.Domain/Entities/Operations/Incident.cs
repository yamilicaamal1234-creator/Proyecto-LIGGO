using System;
using Liggo.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Liggo.Domain.Entities.Operations
{
    public class Incident : BaseEntity
    {
        public Guid PlayerId { get; set; }
        public IncidentType Type { get; set; }
        public string Severity { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IncidentStatus Status { get; set; }
        [NotMapped]
        public IncidentContext Context { get; set; } = new();

        // Navigation property
        public Player Player { get; set; } = null!;
    }

    public class IncidentContext
    {
        public string Student { get; set; } = string.Empty;
        public string Event { get; set; } = string.Empty;
    }
}
