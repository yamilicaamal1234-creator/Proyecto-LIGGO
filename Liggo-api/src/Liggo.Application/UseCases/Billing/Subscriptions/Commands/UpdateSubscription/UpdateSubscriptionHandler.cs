using MediatR;
using Liggo.Application.Interfaces.Billing;

namespace Liggo.Application.UseCases.Billing.Subscriptions.Commands.UpdateSubscription;

public class UpdateSubscriptionHandler : IRequestHandler<UpdateSubscriptionCommand, bool>
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public UpdateSubscriptionHandler(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<bool> Handle(UpdateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (subscription == null) return false;

        subscription.CustomerId = request.CustomerId;
        subscription.PlanId = request.PlanId;
        subscription.Status = request.Status;
        subscription.StartDate = request.StartDate;
        subscription.EndDate = request.EndDate;
        subscription.AutoRenew = request.AutoRenew;

        await _subscriptionRepository.UpdateAsync(subscription, cancellationToken);
        
        return true;
    }
}