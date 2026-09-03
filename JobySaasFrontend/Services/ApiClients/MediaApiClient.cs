namespace JobySaasFrontend.Services;

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
        var response = await _http.GetAsync(
            $"api/Media/{organizationId}/list?pageNumber={pageNumber}&pageSize={pageSize}");

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<List<MediaListDto>>>();
        return result ?? new ServiceResult<List<MediaListDto>> { Success = false, ErrorMessage = "Empty response from API." };
    }

    public async Task<ServiceResult<MediaDetailsDto>> GetMediaDetailsAsync(Guid organizationId, Guid mediaId)
    {
        var response = await _http.GetAsync($"api/Media/{organizationId}/media/{mediaId}");

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<MediaDetailsDto>>();
        return result ?? new ServiceResult<MediaDetailsDto> { Success = false, ErrorMessage = "Empty response from API." };
    }

    public async Task<ServiceResult<Guid>> UploadMediaAsync(Guid organizationId, Stream videoStream, string fileName, string contentType, string title, string? description)
    {
        using var content = new MultipartFormDataContent();

        var videoContent = new StreamContent(videoStream);
        videoContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        content.Add(videoContent, "Video", fileName); // "Video" must match CreateMediaDTO's property name exactly

        content.Add(new StringContent(title), "Title");
        if (!string.IsNullOrEmpty(description))
        {
            content.Add(new StringContent(description), "Description");
        }

        var response = await _http.PostAsync($"api/Media/{organizationId}/media/upload", content);

        var result = await response.Content.ReadFromJsonAsync<ServiceResult<Guid>>();
        return result ?? new ServiceResult<Guid> { Success = false, ErrorMessage = "Empty response from API." };
    }
}