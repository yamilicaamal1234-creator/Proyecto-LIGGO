using MediatR;
using Liggo.Application.Interfaces.Billing;

namespace Liggo.Application.UseCases.Billing.Subscriptions.Commands.DeleteSubscription;

public class DeleteSubscriptionHandler : IRequestHandler<DeleteSubscriptionCommand, bool>
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public DeleteSubscriptionHandler(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<bool> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (subscription == null) return false;

        await _subscriptionRepository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}