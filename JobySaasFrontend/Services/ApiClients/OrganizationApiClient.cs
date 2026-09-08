using System.Net;
using System.Net.Http.Json;
using JobySaasFrontend.Models;
using JobySaasFrontend.Models.DTO;
namespace JobySaasFrontend.Services;

public class OrganizationApiClient : IOrganizationApiClient
{
    private readonly HttpClient _http;

    public OrganizationApiClient(HttpClient http) => _http = http;

    public async Task<ServiceResult<OrganizationDto>> CreateOrganizationAsync(CreateOrganizationRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/Organization/create", request);

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<OrganizationDto>>();

        return result ?? ServiceResult<OrganizationDto>.Fail("Empty response from API.");
    }

    public async Task<ServiceResult<List<OrganizationMemberDTO>>> GetMembersAsync(Guid organizationId)
    {
        var response = await _http.GetAsync($"api/Organization/{organizationId}/members");

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<List<OrganizationMemberDTO>>>();

        return result ?? ServiceResult<List<OrganizationMemberDTO>>.Fail("Empty response from API.");
    }

    public async Task<ServiceResult<List<OrganizationDto>>> GetMyOrganizationsAsync()
    {
        var response = await _http.GetAsync("api/Organization/mine");

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<List<OrganizationDto>>>();

        return result ?? ServiceResult<List<OrganizationDto>>.Fail("Empty response from API.");
    }

    public async Task<ServiceResult<OrganizationDto>> GetOrganizationAsync(Guid organizationId)
    {
        var response = await _http.GetAsync($"api/Organization/{organizationId}/get");

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<OrganizationDto>>();

        return result ?? ServiceResult<OrganizationDto>.Fail("Empty response from API."); ;
    }

    public async Task<ServiceResult<string>> RemoveMemberAsync(Guid organizationId, Guid userId)
    {
        var response = await _http.DeleteAsync($"api/Organization/{organizationId}/members/{userId}/remove");

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<string>>();

        return result ?? ServiceResult<string>.Fail("Empty response from API.");
    }
}