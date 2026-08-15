using JobySaasFrontend.Models.DTO;

namespace JobySaasFrontend.Services;

public interface IAuthService
{
    Task<RegisterResponse> RegisterAsync(RegisterRequest request, string confirmationUrl);
}
