using System;
using Liggo.Domain.Enums;

namespace Liggo.Domain.Entities.Operations
{
    public class Match : BaseEntity
    {
        public string LocalTeam { get; set; } = string.Empty;
        public string VisitingTeam { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
        public string Location { get; set; } = string.Empty;
        public MatchCategory Category { get; set; }
    }
}
