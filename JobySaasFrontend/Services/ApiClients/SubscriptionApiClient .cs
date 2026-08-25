namespace JobySaasFrontend.Services;

// Http/SubscriptionApiClient.cs
using System.Net.Http.Json;
using System.Text.Json;
using JobySaasFrontend.Models.DTO;

public class SubscriptionApiClient : ISubscriptionApiClient
{
    private readonly HttpClient _http;

    public SubscriptionApiClient(HttpClient http) => _http = http;

    public async Task<SubscriptionCheckoutResult> CreateSubscriptionCheckoutAsync(Guid organizationId, Guid subscriptionId)
    {
        var request = new SelectSubscriptionRequest(subscriptionId);

        var response = await _http.PostAsJsonAsync($"api/Subscription/subscription/{organizationId}", request);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<SubscriptionCheckoutResult>() ?? throw new InvalidOperationException("API returned an empty response for CreateSubscriptionCheckout.");
    }

    public async Task<IEnumerable<SubscriptionPlanDto>> GetSubscriptionsAsync()
    {
        var response = await _http.GetAsync("subscriptions/plans");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var plans = JsonSerializer.Deserialize<List<SubscriptionPlanDto>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return plans ?? Enumerable.Empty<SubscriptionPlanDto>();
    }
}