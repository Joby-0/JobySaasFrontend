using JobySaasFrontend.Models.DTO;
using JobySaasFrontend.Services;

public class OrganizationStateService
{
    private readonly IOrganizationApiClient _organizationApi;

    public OrganizationStateService(IOrganizationApiClient organizationApi)
    {
        _organizationApi = organizationApi;
    }

    public List<OrganizationDto> Organizations { get; private set; } = new();

    public OrganizationDto? CurrentOrganization { get; private set; }

    public bool IsLoaded { get; private set; }

    public async Task LoadAsync()
    {
        if (IsLoaded)
            return;

        Organizations = await _organizationApi.GetMyOrganizationsAsync();

        if (Organizations.Count > 0)
        {
            CurrentOrganization = Organizations[0];
        }

        IsLoaded = true;
    }

    public void SelectOrganization(Guid organizationId)
    {
        var organization = Organizations
            .FirstOrDefault(x => x.Id == organizationId);

        if (organization == null)
            return;

        CurrentOrganization = organization;
    }
}