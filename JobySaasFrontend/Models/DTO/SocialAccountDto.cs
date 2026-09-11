namespace JobySaasFrontend.Models.DTO;

public class SocialAccountDto
{
    public Guid Id { get; set; }
    public SocialPlatform Platform { get; set; } // "YouTube", "Instagram", etc.
    public string AccountName { get; set; }
    public string CustomUrl {get; set;}
    public string ProfileImageUrl { get; set; }
    public ulong? Followers {get; set;}
    public SocialAccountStatus Status {get; set;}

    public DateTime LastSync {get; set;}
    public bool IsActive { get; set; }
}
public class SocialAccountDetailsDto
{
    public string AccountName {get; set;}
    public string Handle {get; set;}
    public string ProfileImageUrl {get; set;}
    public ulong? Followers {get; set;}
    public ulong? Views {get; set;}
    public ulong? VideoCount {get; set;}
    public ulong? CommentCount {get;set;}

    public string AccountBanner {get;set;}

    public SocialPlatform Platform {get; set;}

}
public class DailyMetricDto
{
    public DateOnly Date { get; set; }
    public long Views { get; set; }
    public long WatchTimeMinutes { get; set; }
    public long Likes { get; set; }
    public long Comments { get; set; }
    public long Shares { get; set; }
    public long SubscribersGained { get; set; }
}

public class RecentVideoDto
{
    public string VideoId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ThumbnailUrl { get; set; }
    public DateTime PublishedAt { get; set; }

    public ulong Views { get; set; }
    public ulong Likes { get; set; }
    public ulong Comments { get; set; }

    public string Duration { get; set; }
    public string PrivacyStatus { get; set; }
}

public enum SocialAccountStatus
{
    Connected,
    Expired,
    Error,
    Disconnected
}

public enum SocialPlatform
{
    YouTube,
    TikTok,
    X,
    LinkedIn,
    Instagram,
    Facebook
}