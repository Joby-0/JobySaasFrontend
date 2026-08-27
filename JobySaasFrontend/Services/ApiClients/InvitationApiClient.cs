namespace JobySaasFrontend.Services;

using System.Net.Http.Json;
using System.Web;
using JobySaasFrontend.Models;
using JobySaasFrontend.Models.DTO;

public class InvitationApiClient : IInvitationApiClient
{
    private readonly HttpClient _http;

    public InvitationApiClient(HttpClient http) => _http = http;

    public async Task<ServiceResult<string>> CreateInviteCodeAsync(Guid organizationId, int expireInMinutes)
    {
        var response = await _http.PostAsync(
            $"api/Invitation/createinvitecode/{organizationId}?expireInMinutes={expireInMinutes}", null);

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<string>>();
        return result ?? new ServiceResult<string> { Success = false, ErrorMessage = "Empty response from API." };
    }

    public async Task<ServiceResult<InvitationPreviewDto>> GetInvitePreviewAsync(string code)
    {
        var encodedCode = HttpUtility.UrlEncode(code);
        var response = await _http.GetAsync($"api/Invitation/preview?code={encodedCode}");

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<InvitationPreviewDto>>();
        return result ?? new ServiceResult<InvitationPreviewDto> { Success = false, ErrorMessage = "Empty response from API." };
    }
}