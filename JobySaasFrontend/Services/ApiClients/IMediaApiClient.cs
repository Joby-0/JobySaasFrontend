using JobySaasFrontend.Models;
using JobySaasFrontend.Models.DTO;

namespace JobySaasFrontend.Services;

public interface IMediaApiClient
{
    Task<ServiceResult<List<MediaListDto>>> GetMediaListAsync(Guid organizationId, int pageNumber, int pageSize);
    Task<ServiceResult<MediaDetailsDto>> GetMediaDetailsAsync(Guid organizationId, Guid mediaId);
    Task<ServiceResult<Guid>> UploadMediaAsync(Guid organizationId, Stream videoStream, string fileName, string contentType, string title, string? description);
}