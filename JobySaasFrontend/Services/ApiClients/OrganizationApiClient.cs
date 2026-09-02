using System.Net;
using System.Net.Http.Json;
using JobySaasFrontend.Models;
using JobySaasFrontend.Models.DTO;
namespace JobySaasFrontend.Services;

public class OrganizationApiClient : IOrganizationApiClient
{
    private readonly HttpClient _http;

    public OrganizationApiClient(HttpClient http) => _http = http;

    public async Task<OrganizationDto> CreateOrganizationAsync(CreateOrganizationRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/Organization/CreateOrganization", request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<OrganizationDto>() ?? throw new InvalidOperationException("API returned an empty response for CreateOrganization.");
    }

    public async Task<ServiceResult<List<OrganizationMemberDTO>>> GetMembersAsync(Guid organizationId)
    {
        var response = await _http.GetAsync($"api/Organization{organizationId}/members");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ServiceResult<List<OrganizationMemberDTO>>>();
    }

    public async Task<List<OrganizationDto>> GetMyOrganizationsAsync()
    {
        var response = await _http.GetAsync("api/Organization/mine");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<OrganizationDto>>() ?? [];
    }

    public async Task<OrganizationDto?> GetOrganizationAsync(Guid organizationId)
    {
        var response = await _http.PostAsync($"api/Organization/GetOrganization/{organizationId}", null);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<OrganizationDto>();
    }

    public async Task<ServiceResult<string>> RemoveMemberAsync(Guid organizationId, Guid userId)
    {
        var response = await _http.DeleteAsync("api/Organization/{organizationId}/members/{userId}/remove");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ServiceResult<string>>();
    }
}