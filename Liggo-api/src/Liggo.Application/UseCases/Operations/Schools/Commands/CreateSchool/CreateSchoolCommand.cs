using MediatR;
using Liggo.Application.UseCases.Operations.Schools.Dtos;

namespace Liggo.Application.UseCases.Operations.Schools.Commands.CreateSchool;

// El Id viene en el request porque es el ExternalId de MySQL
public record CreateSchoolCommand(
    string Id,
    SchoolInfoDto Info,
    SchoolSettingsDto Settings) : IRequest<string>;