namespace JobySaasFrontend.Services;

// Http/SubscriptionApiClient.cs
using System.Net.Http.Json;
using System.Text.Json;
using JobySaasFrontend.Models;
using JobySaasFrontend.Models.DTO;

public class SubscriptionApiClient : ISubscriptionApiClient
{
    private readonly HttpClient _http;

    public SubscriptionApiClient(HttpClient http) => _http = http;

    public async Task<ServiceResult<string>> CreateSubscriptionCheckoutAsync(Guid organizationId, Guid subscriptionId)
    {
        var request = new SelectSubscriptionRequest(subscriptionId);

        var response = await _http.PostAsJsonAsync($"api/Subscription/subscription/{organizationId}", request);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ServiceResult<string>>() ?? throw new InvalidOperationException("API returned an empty response for CreateSubscriptionCheckout.");
    }

    public async Task<IEnumerable<SubscriptionPlanDto>> GetSubscriptionsAsync()
    {
        var response = await _http.GetAsync("api/Subscription/plans");
        var content = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ApiResponse<List<SubscriptionPlanDto>>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (result is null || !result.Success)
        {
            throw new InvalidOperationException(result?.Error ?? "Failed to load subscription plans.");
        }

        return result.Data ?? [];
    }
}