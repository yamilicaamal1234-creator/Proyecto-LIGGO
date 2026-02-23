using MediatR;
using Liggo.Application.Interfaces.Billing;

namespace Liggo.Application.UseCases.Billing.Subscriptions.Queries.GetSubscriptionById;

public class GetSubscriptionByIdHandler : IRequestHandler<GetSubscriptionByIdQuery, SubscriptionResponse?>
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public GetSubscriptionByIdHandler(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<SubscriptionResponse?> Handle(GetSubscriptionByIdQuery request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(request.Id, cancellationToken);

        if (subscription == null) return null;

        return new SubscriptionResponse(
            subscription.Id,
            subscription.CustomerId,
            subscription.PlanId,
            subscription.Status,
            subscription.StartDate,
            subscription.EndDate,
            subscription.AutoRenew);
    }
}