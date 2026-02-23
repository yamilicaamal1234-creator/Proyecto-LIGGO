namespace Liggo.Application.UseCases.Operations.Teams.Dtos;

public record TeamStatsDto(
    int Won, 
    int Lost, 
    int Tied);