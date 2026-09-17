using JobySaasFrontend.Models;
using JobySaasFrontend.Models.DTO;

namespace JobySaasFrontend.Services;

public interface IInvitationApiClient
{
    Task<ServiceResult<InvitationDto>> CreateInviteCodeAsync(Guid organizationId, int expireInMinutes, string? email);
    Task<ServiceResult<InvitationPreviewDto>> GetInvitePreviewAsync(string code);
    Task<ServiceResult<bool>> AcceptInviteAsync(string code);
}