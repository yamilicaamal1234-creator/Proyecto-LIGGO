using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;

namespace Liggo.Application.UseCases.Operations.Schools.Commands.DeleteSchool;

public class DeleteSchoolHandler : IRequestHandler<DeleteSchoolCommand, bool>
{
    private readonly ISchoolRepository _schoolRepository;

    public DeleteSchoolHandler(ISchoolRepository schoolRepository)
    {
        _schoolRepository = schoolRepository;
    }

    public async Task<bool> Handle(DeleteSchoolCommand request, CancellationToken cancellationToken)
    {
        var school = await _schoolRepository.GetByIdAsync(Guid.Parse(request.Id), Guid.Empty);
        if (school == null) return false;

        await _schoolRepository.DeleteAsync(Guid.Parse(request.Id), Guid.Empty);
        return true;
    }
}