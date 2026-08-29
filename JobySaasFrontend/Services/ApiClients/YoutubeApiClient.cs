namespace JobySaasFrontend.Services;

using System.Net.Http.Headers;
using System.Net.Http.Json;
using JobySaasFrontend.Models.DTO;

public class YoutubeApiClient : IYoutubeApiClient
{
    private readonly HttpClient _http;

    public YoutubeApiClient(HttpClient http) => _http = http;

    public async Task<string> GetConnectUrlAsync(Guid organizationId)
    {
        var response = await _http.GetAsync(
            $"api/youtube/connect?organizationId={organizationId}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<UploadResult> UploadVideoAsync(Stream videoStream, string fileName, string contentType, string title, string description, string categoryId, Guid socialAccountId)
    {
        using var content = new MultipartFormDataContent();

        var videoContent = new StreamContent(videoStream);
        videoContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        content.Add(videoContent, "video", fileName);

        content.Add(new StringContent(title), "title");
        content.Add(new StringContent(description), "description");
        content.Add(new StringContent(categoryId), "categoryId");
        content.Add(new StringContent(socialAccountId.ToString()), "socialAccountId"); // once the API takes an ID instead of a full object

        var response = await _http.PostAsync("api/youtube/upload", content);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<UploadResult>() ?? throw new InvalidOperationException("API returned an empty response for video upload.");
    }
}