using JobySaasFrontend.Models;
using JobySaasFrontend.Models.DTO;

namespace JobySaasFrontend.Services;

public interface ISocialAccountApiClient
{
    Task<ServiceResult<List<SocialAccountDto>>> GetConnectedAccountsAsync(Guid organizationId);
    Task<ServiceResult<bool>> DisconnectAccountAsync(Guid organizationId, Guid accountId);
    Task<ServiceResult<SocialAccountDetailsDto>> GetAccountDetailsAsync(Guid organizationId, Guid accountId);
    Task<ServiceResult<List<DailyMetricDto>>> GetAccountPerformanceAsync(Guid orgId, Guid accountId, DateOnly startDate, DateOnly endDate, CancellationToken ct);
    Task<ServiceResult<PagedResult<RecentVideoDto>>> GetAccountVideosAsync(Guid orgId, Guid accountId, int pageNumber, int pageSize, string order, CancellationToken ct);
}