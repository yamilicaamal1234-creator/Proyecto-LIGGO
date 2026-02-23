using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.Schools.Dtos;
using Liggo.Application.UseCases.Operations.Schools.Queries.Responses;

namespace Liggo.Application.UseCases.Operations.Schools.Queries.GetSchoolById;

public class GetSchoolByIdHandler : IRequestHandler<GetSchoolByIdQuery, SchoolResponse?>
{
    private readonly ISchoolRepository _schoolRepository;

    public GetSchoolByIdHandler(ISchoolRepository schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }

    public async Task<SchoolResponse?> Handle(GetSchoolByIdQuery request, CancellationToken cancellationToken)
    {
        var school = await _schoolRepository.GetByIdAsync(request.Id, cancellationToken);
        if (school == null) return null;

        return new SchoolResponse(
            school.Id,
            new SchoolInfoDto(school.Info.Name, school.Info.Plan, school.Info.LogoUrl),
            new SchoolSettingsDto(
                school.Settings.Currency, 
                school.Settings.Timezone, 
                school.Settings.Categories.Select(c => new CategoryInfoDto(c.Name, c.Years)).ToList()
            )
        );
    }
}