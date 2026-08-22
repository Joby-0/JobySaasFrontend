using System.Net.Http.Json;
using JobySaasFrontend.Models.DTO;
using Microsoft.AspNetCore.Components;

namespace JobySaasFrontend.Services;

public sealed class AuthApiClient(HttpClient httpClient, NavigationManager navigationManager) : IAuthApiClient
{
    public async Task<ConfirmEmailResponse> ConfirmEmailAsync(
        string userId,
        string code,
        CancellationToken cancellationToken = default)
    {
        var endpoint = new Uri(new Uri(navigationManager.BaseUri), "api/auth/confirm-email");
        using var response = await httpClient.PostAsJsonAsync(endpoint,
            new ConfirmEmailRequest { UserId = userId, Code = code },
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return new ConfirmEmailResponse();
        }

        return await response.Content.ReadFromJsonAsync<ConfirmEmailResponse>(cancellationToken)
            ?? new ConfirmEmailResponse();
    }
}
