using JobySaasFrontend.Models.DTO;

namespace JobySaasFrontend.Services;

public interface ISubscriptionApiClient
{
    Task<SubscriptionCheckoutResult> CreateSubscriptionCheckoutAsync(Guid organizationId, Guid subscriptionId);
    Task<IEnumerable<SubscriptionPlanDto>> GetSubscriptionsAsync();
}