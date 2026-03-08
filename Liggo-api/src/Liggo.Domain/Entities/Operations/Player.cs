using System;
using System.Collections.Generic;

namespace Liggo.Domain.Entities.Operations
{
    public class Player : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string AssignedTeam { get; set; } = string.Empty;
        public string GuardianName { get; set; } = string.Empty;
        public string GuardianPhone { get; set; } = string.Empty;
        public string Relationship { get; set; } = string.Empty;

        // Propiedad de navegación para la relación con Inscripciones
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}
