using JobySaasFrontend.Models;
using JobySaasFrontend.Models.DTO;

namespace JobySaasFrontend.Services;
public interface IOrganizationApiClient
{
    Task<OrganizationDto> CreateOrganizationAsync(CreateOrganizationRequest request);
    Task<OrganizationDto?> GetOrganizationAsync(Guid organizationId);

    Task<List<OrganizationDto?>> GetMyOrganizationsAsync();

    Task<ServiceResult<List<OrganizationMemberDTO>>> GetMembersAsync(Guid organizationId);

    Task<ServiceResult<string>> RemoveMemberAsync(Guid organizationId, Guid userId);
}