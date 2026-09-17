namespace JobySaasFrontend.Services;

using System.Net;
using System.Net.Http.Json;
using System.Web;
using JobySaasFrontend.Models;
using JobySaasFrontend.Models.DTO;

public class InvitationApiClient : IInvitationApiClient
{
    private readonly HttpClient _http;

    public InvitationApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<ServiceResult<bool>> AcceptInviteAsync(string code)
    {
        var response = await _http.GetAsync($"api/Invitation/accept?code={Uri.EscapeDataString(code)}");

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<bool>>();

        return result ?? ServiceResult<bool>.Fail("Empty response from API.");
    }

    public async Task<ServiceResult<InvitationDto>> CreateInviteCodeAsync(Guid organizationId, int expireInMinutes, string? email)
    {
        var response = await _http.GetAsync(
        $"api/Invitation/{organizationId}/create" +
        $"?expireInMinutes={expireInMinutes}" +
        $"&email={Uri.EscapeDataString(email ?? "")}");
        var result = await response.Content.ReadFromJsonAsync<ServiceResult<InvitationDto>>();

        return result ?? ServiceResult<InvitationDto>.Fail("Empty response from API.");
    }

    public async Task<ServiceResult<InvitationPreviewDto>> GetInvitePreviewAsync(string code)
    {
        var encodedCode = Uri.EscapeDataString(code);

        var response = await _http.GetAsync($"api/Invitation/preview?code={encodedCode}");

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<InvitationPreviewDto>>();

        return result ?? ServiceResult<InvitationPreviewDto>.Fail("Empty response from API.");
    }
}