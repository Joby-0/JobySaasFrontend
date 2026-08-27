using System.Net.Http.Headers;

namespace JobySaasFrontend.Encryption;

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

        if (!string.IsNullOrEmpty(cookie))
        {
            var jwtUserToken = _encryptions.AesDecryptFromBase64<JwtUserToken>(cookie);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtUserToken.EncryptedToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}