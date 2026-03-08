using System;
using System.ComponentModel.DataAnnotations;
using Liggo.Domain.Enums;

namespace Liggo.Api.Controllers.Operations.Matches.Contracts
{
    public class CreateMatchRequest
    {
        [Required]
        public string LocalTeam { get; set; } = string.Empty;

        [Required]
        public string VisitingTeam { get; set; } = string.Empty;

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public MatchCategory Category { get; set; }
    }
}
