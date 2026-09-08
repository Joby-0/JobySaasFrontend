using JobySaasFrontend.Models;
using JobySaasFrontend.Models.DTO;

namespace JobySaasFrontend.Services;


public class SocialAccountApiClient : ISocialAccountApiClient
{
    private readonly HttpClient _http;
    public SocialAccountApiClient(HttpClient http) => _http = http;

    public async Task<ServiceResult<bool>> DisconnectAccountAsync(Guid organizationId, Guid accountId)
    {
        var response = await _http.DeleteAsync($"api/SocialAccount/{organizationId}/disconnect?accountId={accountId}");

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<bool>>();

        return result ?? ServiceResult<bool>.Fail("Empty response.");
    }

    public async Task<ServiceResult<SocialAccountDetailsDto>> GetAccountDetailsAsync( Guid organizationId,Guid accountId)
    {
        var response = await _http.GetAsync($"api/SocialAccount/{organizationId}/details?accountId={accountId}");

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<SocialAccountDetailsDto>>();

        return result ?? ServiceResult<SocialAccountDetailsDto>.Fail("Empty response.");
    }

    public async Task<ServiceResult<List<SocialAccountDto>>> GetConnectedAccountsAsync(Guid organizationId)
    {
        var response = await _http.GetAsync($"api/SocialAccount/{organizationId}/mine");

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<List<SocialAccountDto>>>();

        return result ?? ServiceResult<List<SocialAccountDto>>.Fail("Empty response.");
    }
}