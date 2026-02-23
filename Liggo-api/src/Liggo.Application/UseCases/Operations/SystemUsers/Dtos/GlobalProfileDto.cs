namespace Liggo.Application.UseCases.Operations.SystemUsers.Dtos;

public record GlobalProfileDto(
    string FullName, 
    string Phone, 
    string PhotoUrl);