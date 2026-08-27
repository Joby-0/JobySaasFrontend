using JobySaasFrontend.Models.DTO;

namespace JobySaasFrontend.Services;
public interface IOrganizationApiClient
{
    Task<OrganizationDto> CreateOrganizationAsync(CreateOrganizationRequest request);
    Task<OrganizationDto?> GetOrganizationAsync(Guid organizationId);
}