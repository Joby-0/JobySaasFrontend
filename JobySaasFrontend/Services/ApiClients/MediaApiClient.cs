namespace JobySaasFrontend.Services;

using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using JobySaasFrontend.Models;
using JobySaasFrontend.Models.DTO;

public class MediaApiClient : IMediaApiClient
{
    private readonly HttpClient _http;

    public MediaApiClient(HttpClient http) => _http = http;

    public async Task<ServiceResult<List<MediaListDto>>> GetMediaListAsync(Guid organizationId, int pageNumber, int pageSize)
    {
        var url =
            $"api/Media/{organizationId}/list" +
            $"?pageNumber={pageNumber}" +
            $"&pageSize={pageSize}";

        var response = await _http.GetAsync(url);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return ServiceResult<List<MediaListDto>>.Fail("You are not authenticated.");
        }

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<List<MediaListDto>>>();

        return result ?? ServiceResult<List<MediaListDto>>.Fail("Empty response from API.");
    }
    public async Task<ServiceResult<MediaDetailsDto>> GetMediaDetailsAsync(Guid organizationId, Guid mediaId)
    {
        var response = await _http.GetAsync($"api/Media/{organizationId}/media/{mediaId}");

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return ServiceResult<MediaDetailsDto>.Fail("You are not authenticated.");
        }

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<MediaDetailsDto>>();

        return result ?? ServiceResult<MediaDetailsDto>.Fail("Empty response from API.");
    }

    public async Task<ServiceResult<Guid>> UploadMediaAsync(Guid organizationId, Stream videoStream, string fileName, string contentType, string title, string? description)
    {
        using var content = new MultipartFormDataContent();

        var videoContent = new StreamContent(videoStream);
        videoContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        content.Add(videoContent, "Video", fileName);

        content.Add(new StringContent(title), "Title");
        if (!string.IsNullOrEmpty(description))
        {
            content.Add(new StringContent(description), "Description");
        }

        var response = await _http.PostAsync($"api/Media/{organizationId}/media/upload", content);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return ServiceResult<Guid>.Fail("You are not authenticated.");
        }

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<Guid>>();

        return result ?? ServiceResult<Guid>.Fail("Empty response from API.");
    }

    public async Task<ServiceResult<Guid>> PublishMediaAsync(Guid organizationId, Guid mediaId, List<Guid> socialAccountIds)
    {
        var response = await _http.PostAsJsonAsync($"api/Media/{organizationId}/media/{mediaId}/publish", socialAccountIds);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return ServiceResult<Guid>.Fail("You are not authenticated.");
        }
        
        return await response.Content.ReadFromJsonAsync<ServiceResult<Guid>>() ?? ServiceResult<Guid>.Fail("Empty response from API.");
    }
}