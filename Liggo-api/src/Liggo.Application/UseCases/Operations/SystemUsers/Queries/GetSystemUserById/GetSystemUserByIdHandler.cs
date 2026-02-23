using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Liggo.Application.Interfaces.Operations;
using Liggo.Application.UseCases.Operations.SystemUsers.Dtos;
using Liggo.Application.UseCases.Operations.SystemUsers.Queries.Responses;

namespace Liggo.Application.UseCases.Operations.SystemUsers.Queries.GetSystemUserById;

public class GetSystemUserByIdHandler : IRequestHandler<GetSystemUserByIdQuery, SystemUserResponse?>
{
    private readonly ISystemUserRepository _repository;

    public GetSystemUserByIdHandler(ISystemUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<SystemUserResponse?> Handle(GetSystemUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (user == null) return null;

        return new SystemUserResponse(
            user.Id,
            new AuthDataDto(user.Auth.Email, user.Auth.Provider, user.Auth.EmailVerified, user.Auth.AccountStatus, user.Auth.LastLogin),
            new GlobalProfileDto(user.GlobalProfile.FullName, user.GlobalProfile.Phone, user.GlobalProfile.PhotoUrl),
            user.ActiveTenantId,
            user.Tenants ?? new System.Collections.Generic.List<string>(),
            user.CreatedAt
        );
    }
}