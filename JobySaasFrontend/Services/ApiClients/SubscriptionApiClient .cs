namespace JobySaasFrontend.Services;

// Http/SubscriptionApiClient.cs
using System.Net.Http.Json;
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
}