using System;

namespace Liggo.Application.UseCases.Operations.Matches.Dtos
{
    public class MatchDto
    {
        public Guid Id { get; set; }
        public string LocalTeam { get; set; } = string.Empty;
        public string VisitingTeam { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}
