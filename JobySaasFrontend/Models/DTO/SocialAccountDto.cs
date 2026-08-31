namespace JobySaasFrontend.Models.DTO;

public class SocialAccountDto
{
    public Guid Id { get; set; }
    public SocialAccountPlatfrom Platform { get; set; } // "YouTube", "Instagram", etc.
    public string AccountName { get; set; }
    public string CostumUrl {get; set;}
    public string ProfileImageUrl { get; set; }
    public ulong? Followers {get; set;}
    public SocialAccountStatus Status {get; set;}

    public DateTime LastSync {get; set;}
    public bool IsActive { get; set; }
}

public enum SocialAccountStatus
{
    Connected,
    Expired,
    Error,
    Disconnected
}

public enum SocialAccountPlatfrom
{
    YouTube,
    TikTok,
    X,
    LinkedIn,
    Instagram,
    Facebook
}