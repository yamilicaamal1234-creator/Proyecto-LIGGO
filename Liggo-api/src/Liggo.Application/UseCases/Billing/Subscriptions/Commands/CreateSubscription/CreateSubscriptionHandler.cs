using MediatR;
using Liggo.Application.Interfaces.Billing;
using Liggo.Domain.Entities.Billing;

namespace Liggo.Application.UseCases.Billing.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionHandler : IRequestHandler<CreateSubscriptionCommand, int>
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public CreateSubscriptionHandler(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<int> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = new Subscription
        {
            CustomerId = request.CustomerId,
            PlanId = request.PlanId,
            Status = request.Status,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            AutoRenew = request.AutoRenew
        };

        await _subscriptionRepository.AddAsync(subscription, cancellationToken);

        return subscription.Id;
    }
}