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
        var school = await _schoolRepository.GetByIdAsync(request.Id, cancellationToken);
        if (school == null) return false;

        await _schoolRepository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}