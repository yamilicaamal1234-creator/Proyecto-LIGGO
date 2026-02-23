using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Domain.Entities.Operations;

namespace Liggo.Application.UseCases.Operations.Schools.Commands.CreateSchool;

public class CreateSchoolHandler : IRequestHandler<CreateSchoolCommand, string>
{
    private readonly ISchoolRepository _schoolRepository;

    public CreateSchoolHandler(ISchoolRepository schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }

    public async Task<string> Handle(CreateSchoolCommand request, CancellationToken cancellationToken)
    {
        var school = new School
        {
            Id = request.Id, // Usamos el ID exacto que nos pasan
            Info = new SchoolInfo
            {
                Name = request.Info.Name,
                Plan = request.Info.Plan,
                LogoUrl = request.Info.LogoUrl
            },
            Settings = new SchoolSettings
            {
                Currency = string.IsNullOrWhiteSpace(request.Settings.Currency) ? "MXN" : request.Settings.Currency,
                Timezone = string.IsNullOrWhiteSpace(request.Settings.Timezone) ? "America/Mexico_City" : request.Settings.Timezone,
                Categories = request.Settings.Categories?.Select(c => new CategoryInfo
                {
                    Name = c.Name,
                    Years = c.Years ?? new System.Collections.Generic.List<int>()
                }).ToList() ?? new System.Collections.Generic.List<CategoryInfo>()
            }
        };

        await _schoolRepository.AddAsync(school, cancellationToken);

        return school.Id;
    }
}