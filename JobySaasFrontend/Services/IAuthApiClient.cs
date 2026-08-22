using JobySaasFrontend.Models.DTO;

namespace JobySaasFrontend.Services;

public interface IAuthApiClient
{
    Task<ConfirmEmailResponse> ConfirmEmailAsync(string userId, string code, CancellationToken cancellationToken = default);
}
