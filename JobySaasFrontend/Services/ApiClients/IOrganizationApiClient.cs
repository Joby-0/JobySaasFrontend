using JobySaasFrontend.Models;
using JobySaasFrontend.Models.DTO;

namespace JobySaasFrontend.Services;

public interface IOrganizationApiClient
{
    Task<ServiceResult<OrganizationDto>> CreateOrganizationAsync(CreateOrganizationRequest request);
    Task<ServiceResult<OrganizationDto>> GetOrganizationAsync(Guid organizationId);

    Task<ServiceResult<List<OrganizationDto>>> GetMyOrganizationsAsync();

    Task<ServiceResult<List<OrganizationMemberDTO>>> GetMembersAsync(Guid organizationId);

    Task<ServiceResult<string>> RemoveMemberAsync(Guid organizationId, Guid userId);
}