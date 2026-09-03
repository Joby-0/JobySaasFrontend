namespace JobySaasFrontend.Models.DTO;

public class MediaListDto
{
    public Guid Id { get; set; }
    public string ThumbnailUrl { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TimeSpan? Duration { get; set; }
    public DateTime CreatedAt { get; set; }

    public int SocialAccountCount { get; set; }
    public List<SocialPlatform> SocialPlatforms { get; set; }
}


public class MediaDetailsDto
{
    public Guid Id { get; set; }
    public string ThumbnailUrl { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TimeSpan? Duration { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<SocialAccountDto> SocialAccounts { get; set; } //maybe u want less info here, like only the platform and the account name, not the whole social account dto
    // public List<ISocialVideo> SocialVideos { get; set; } //maybe u want less info here, do a dto for this,
}

public class CreateMediadto
{
    public IFormFile Video { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CategoryId { get; set; }
    public string MediaId { get; set; }
}