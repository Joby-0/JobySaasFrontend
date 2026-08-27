using JobySaasFrontend.Models.DTO;

namespace JobySaasFrontend.Services;
public interface IYoutubeApiClient
{
    Task<string> GetConnectUrlAsync();
    Task<UploadResult> UploadVideoAsync(Stream videoStream, string fileName, string contentType, string title, string description, string categoryId, Guid socialAccountId);
}