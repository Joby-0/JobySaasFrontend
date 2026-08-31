using System.Net.Http.Headers;
using JobySaasFrontend.Data;
using JobySaasFrontend.Encryption;
using JobySaasFrontend.Models.DTO;
using JobySaasFrontend.Services;
using Microsoft.AspNetCore.Identity;

public class JwtAuthHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly Encryptions _encryptions;
    private readonly JWTService _jwtService;
    private readonly UserManager<ApplicationUser> _userManager;

    public JwtAuthHandler(IHttpContextAccessor contextAccessor, Encryptions encryptions, JWTService jwtSerivce, UserManager<ApplicationUser> userManager)
    {
        _contextAccessor = contextAccessor;
        _encryptions = encryptions;
        _jwtService = jwtSerivce;
        _userManager = userManager;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var cookie = _contextAccessor.HttpContext?.Request.Cookies["joby_api_token"];

        JwtUserToken? jwtUserToken = null;

        if (!string.IsNullOrEmpty(cookie))
        {
            jwtUserToken = _encryptions.AesDecryptFromBase64<JwtUserToken>(cookie);
        }

        // No API token OR token is expired/about to expire
        if (jwtUserToken == null || jwtUserToken.ExpireTime <= DateTime.UtcNow.AddMinutes(5))
        {
            try
            {
                jwtUserToken = await RenewAsync();
            }
            catch
            {
                // Renewal failed.
                // The Identity session may also have expired.
                jwtUserToken = null;
            }
        }

        if (jwtUserToken != null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtUserToken.EncryptedToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<JwtUserToken?> RenewAsync()
    {
        var httpContext = _contextAccessor.HttpContext;

        if (httpContext?.User?.Identity?.IsAuthenticated != true)
            return null;

        var user = await _userManager.GetUserAsync(httpContext.User);

        if (user == null)
            return null;

        return await _jwtService.CreateJwtUserTokenAsync(user);
    }
}