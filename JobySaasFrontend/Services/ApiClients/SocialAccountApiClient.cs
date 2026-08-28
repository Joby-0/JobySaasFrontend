using JobySaasFrontend.Models;
using JobySaasFrontend.Models.DTO;

namespace JobySaasFrontend.Services;


public class SocialAccountApiClient : ISocialAccountApiClient
{
    private readonly HttpClient _http;
    public SocialAccountApiClient(HttpClient http) => _http = http;

    public async Task<ServiceResult<List<SocialAccountDto>>> GetConnectedAccountsAsync(Guid organizationId)
    {
        var response = await _http.GetAsync($"api/SocialAccount/mine/{organizationId}");
        var result = await response.Content.ReadFromJsonAsync<ServiceResult<List<SocialAccountDto>>>();
        return result ?? new ServiceResult<List<SocialAccountDto>> { Success = false, ErrorMessage = "Empty response." };
    }
}