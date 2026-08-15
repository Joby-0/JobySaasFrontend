using JobySaasFrontend.Models.DTO;

namespace JobySaasFrontend.Services;

public interface IAuthService
{
    public Task<RegisterResponse> RegisterAsync(RegisterRequest request);
}