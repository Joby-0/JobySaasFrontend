using JobySaasFrontend.Models;
using JobySaasFrontend.Models.DTO;

namespace JobySaasFrontend.Services;


public class SocialAccountApiClient : ISocialAccountApiClient
{
    private readonly HttpClient _http;
    public SocialAccountApiClient(HttpClient http) => _http = http;

    public async Task<ServiceResult<bool>> DisconnectAccountAsync(Guid organizationId, Guid accountId)
    {
        var response = await _http.GetAsync($"api/SocialAccount/{organizationId}/disconnect?accountId={accountId}");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ServiceResult<bool>>() ?? new ServiceResult<bool> { Success = false, ErrorMessage = "Empty response." };
    }

    public async Task<ServiceResult<List<SocialAccountDto>>> GetConnectedAccountsAsync(Guid organizationId)
    {
        var response = await _http.GetAsync($"api/SocialAccount/{organizationId}/mine");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ServiceResult<List<SocialAccountDto>>>();
        return result ?? new ServiceResult<List<SocialAccountDto>> { Success = false, ErrorMessage = "Empty response." };
    }
}