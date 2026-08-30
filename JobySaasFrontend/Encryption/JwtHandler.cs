using System.Net.Http.Headers;
using JobySaasFrontend.Encryption;
using JobySaasFrontend.Services;

public class JwtAuthHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly Encryptions _encryptions;

    public JwtAuthHandler(IHttpContextAccessor contextAccessor, Encryptions encryptions)
    {
        _contextAccessor = contextAccessor;
        _encryptions = encryptions;
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
                // jwtUserToken = await _apiTokenService.RenewAsync();
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
}