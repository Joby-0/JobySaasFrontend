namespace JobySaasFrontend.Services;

using System.Net;
using System.Net.Http.Json;
using System.Web;
using JobySaasFrontend.Models;
using JobySaasFrontend.Models.DTO;

public class InvitationApiClient : IInvitationApiClient
{
    private readonly HttpClient _http;

    public InvitationApiClient(HttpClient http) => _http = http;

    public async Task<ServiceResult<bool>> AcceptInviteAsync(string code)
    {
        var response = await _http.GetAsync($"api/Invitation/accept?code={Uri.EscapeDataString(code)}");

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return ServiceResult<bool>.Fail("You are not authenticated.");
        }

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<bool>>();

        return result ?? ServiceResult<bool>.Fail("Empty response from API.");
    }

    public async Task<ServiceResult<string>> CreateInviteCodeAsync(Guid organizationId, int expireInMinutes)
    {
        var response = await _http.PostAsync($"api/Invitation/createinvitecode/{organizationId}?expireInMinutes={expireInMinutes}", null);

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<string>>();

        if (result is null)
        {
            return ServiceResult<string>.Fail("Empty response from API.");
        }

        return result;
    }

    public async Task<ServiceResult<InvitationPreviewDto>> GetInvitePreviewAsync(string code)
    {
        var encodedCode = Uri.EscapeDataString(code);

        var response = await _http.GetAsync($"api/Invitation/preview?code={encodedCode}");

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<InvitationPreviewDto>>();

        return result ??  ServiceResult<InvitationPreviewDto>.Fail("Empty response from API.");
    }
}