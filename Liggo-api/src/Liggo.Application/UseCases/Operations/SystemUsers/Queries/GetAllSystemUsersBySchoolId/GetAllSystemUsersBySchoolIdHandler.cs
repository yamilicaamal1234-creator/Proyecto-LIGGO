using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.SystemUsers.Dtos;
using Liggo.Application.UseCases.Operations.SystemUsers.Queries.Responses;

namespace Liggo.Application.UseCases.Operations.SystemUsers.Queries.GetAllSystemUsersBySchoolId;

public class GetAllSystemUsersBySchoolIdHandler : IRequestHandler<GetAllSystemUsersBySchoolIdQuery, IEnumerable<SystemUserResponse>>
{
    private readonly ISystemUserRepository _repository;

    public GetAllSystemUsersBySchoolIdHandler(ISystemUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SystemUserResponse>> Handle(GetAllSystemUsersBySchoolIdQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.GetAllBySchoolIdAsync(request.SchoolId, cancellationToken);
        
        if (users == null || !users.Any()) return System.Linq.Enumerable.Empty<SystemUserResponse>();

        return users.Select(user => new SystemUserResponse(
            user.Id,
            new AuthDataDto(user.Auth.Email, user.Auth.Provider, user.Auth.EmailVerified, user.Auth.AccountStatus, user.Auth.LastLogin),
            new GlobalProfileDto(user.GlobalProfile.FullName, user.GlobalProfile.Phone, user.GlobalProfile.PhotoUrl),
            user.ActiveTenantId,
            user.Tenants ?? new System.Collections.Generic.List<string>(),
            user.CreatedAt
        ));
    }
}