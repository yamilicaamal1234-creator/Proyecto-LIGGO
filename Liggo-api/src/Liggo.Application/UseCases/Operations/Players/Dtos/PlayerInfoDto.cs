namespace Liggo.Application.UseCases.Operations.Players.Dtos;

public record PlayerInfoDto(
    string Name, 
    string Dob, 
    string Gender, 
    string PhotoUrl);