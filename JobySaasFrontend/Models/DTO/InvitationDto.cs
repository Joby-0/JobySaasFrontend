namespace JobySaasFrontend.Models.DTO;
public class InvitationPreviewDto
{
    public string OrganizationName { get; set; }
    public string InvitedByName { get; set; }
    public DateTime ExpiresAt { get; set; }
}

public class InvitationDto
{
    public string Code {get; set;}
    public DateTime ExpiresAt {get; set;}
}