using JobySaasFrontend.Models;
using JobySaasFrontend.Models.DTO;

namespace JobySaasFrontend.Services;

public interface IInvitationApiClient
{
    Task<ServiceResult<string>> CreateInviteCodeAsync(Guid organizationId, int expireInMinutes);
    Task<ServiceResult<InvitationPreviewDto>> GetInvitePreviewAsync(string code);
    Task<ServiceResult<bool>> AcceptInviteAsync(string code);
}