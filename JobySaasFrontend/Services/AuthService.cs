using JobySaasFrontend.Data;
using JobySaasFrontend.Models.DTO;

namespace JobySaasFrontend.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ApplicationDbContext _dbContext;
    public AuthService(HttpClient httpClient, ApplicationDbContext dbcontext)
    {
        _httpClient = httpClient;
        _dbContext = dbcontext;
    }
    public Task<RegisterResponse> RegisterAsync(RegisterRequest request)
    {
        throw new NotImplementedException();
    }
}