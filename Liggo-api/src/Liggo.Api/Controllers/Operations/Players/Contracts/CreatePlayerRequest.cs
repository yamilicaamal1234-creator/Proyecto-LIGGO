using System;
using System.ComponentModel.DataAnnotations;

namespace Liggo.Api.Controllers.Operations.Players.Contracts
{
    public class CreatePlayerRequest
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        public string AssignedTeam { get; set; } = string.Empty;

        [Required]
        public string GuardianName { get; set; } = string.Empty;

        [Required]
        public string GuardianPhone { get; set; } = string.Empty;

        public string Relationship { get; set; } = string.Empty;
    }
}
