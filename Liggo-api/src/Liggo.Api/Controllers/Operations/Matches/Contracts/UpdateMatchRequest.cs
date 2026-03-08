namespace Liggo.Api.Controllers.Operations.Matches.Contracts;

public record UpdateMatchRequest(
    string LocalTeam,
    string VisitingTeam,
    DateTime DateTime,
    string Location,
    string Category);