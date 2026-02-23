namespace Liggo.Application.UseCases.Operations.Players.Dtos;

public record PlayerStatsDto(
    int Matches, 
    int Goals, 
    int Minutes, 
    int YellowCards, 
    int RedCards);