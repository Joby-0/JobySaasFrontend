namespace JobySaasFrontend.Models.DTO;

public record CreateOrganizationRequest(string Name);

public class OrganizationDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
}