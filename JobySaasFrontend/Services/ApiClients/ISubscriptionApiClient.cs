using JobySaasFrontend.Models;
using JobySaasFrontend.Models.DTO;

namespace JobySaasFrontend.Services;

public interface ISubscriptionApiClient
{
    Task<ServiceResult<string>> CreateSubscriptionCheckoutAsync(Guid organizationId, Guid subscriptionId);
    Task<IEnumerable<SubscriptionPlanDto>> GetSubscriptionsAsync();
}