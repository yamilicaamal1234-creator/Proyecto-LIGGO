using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;

namespace Liggo.Application.UseCases.Operations.Schools.Commands.UpdateSchool;

public class UpdateSchoolHandler : IRequestHandler<UpdateSchoolCommand, bool>
{
    private readonly ISchoolRepository _schoolRepository;

    public UpdateSchoolHandler(ISchoolRepository schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }

    public async Task<bool> Handle(UpdateSchoolCommand request, CancellationToken cancellationToken)
    {
        var school = await _schoolRepository.GetByIdAsync(Guid.Parse(request.Id), Guid.Empty);
        if (school == null) return false;

        school.Info = new Liggo.Domain.Entities.Operations.SchoolInfo
        {
            Name = request.Info.Name,
            Plan = request.Info.Plan,
            LogoUrl = request.Info.LogoUrl
        };

        school.Settings.Currency = request.Settings.Currency;
        school.Settings.Timezone = request.Settings.Timezone;
        school.Settings.Categories = request.Settings.Categories?.Select(c => new Liggo.Domain.Entities.Operations.CategoryInfo
        {
            Name = c.Name,
            Years = c.Years ?? new System.Collections.Generic.List<int>()
        }).ToList() ?? new System.Collections.Generic.List<Liggo.Domain.Entities.Operations.CategoryInfo>();

        await _schoolRepository.UpdateAsync(school);
        
        return true;
    }
}