using System;

namespace Liggo.Application.UseCases.Operations.Players.Dtos
{
    public class PlayerDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string AssignedTeam { get; set; } = string.Empty;
        public string GuardianName { get; set; } = string.Empty;
        public string GuardianPhone { get; set; } = string.Empty;
        public string Relationship { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
