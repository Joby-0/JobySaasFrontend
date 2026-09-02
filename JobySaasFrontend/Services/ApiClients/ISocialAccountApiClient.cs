using JobySaasFrontend.Models;
using JobySaasFrontend.Models.DTO;

namespace JobySaasFrontend.Services;

public interface ISocialAccountApiClient
{
    Task<ServiceResult<List<SocialAccountDto>>> GetConnectedAccountsAsync(Guid organizationId);
    Task<ServiceResult<bool>> DisconnectAccountAsync(Guid organizationId, Guid accountId);
}