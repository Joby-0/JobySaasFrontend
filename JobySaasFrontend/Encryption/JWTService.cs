using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;

using JobySaasFrontend.Encryption.Options;
using JobySaasFrontend.Models.DTO;
using JobySaasFrontend.Data;
using Microsoft.AspNetCore.Identity;

namespace JobySaasFrontend.Encryption;

public class JWTService
{
    private readonly JwtOptions _jwtOptions;
    private readonly UserManager<ApplicationUser> _userManager;


    public JWTService(IOptions<JwtOptions> jwtOptions,UserManager<ApplicationUser> userManager)
    {
        _jwtOptions = jwtOptions.Value;
        _userManager = userManager;
    }

    //Create a list of claims to encrypt into the JWT token
    private IEnumerable<Claim> CreateClaims(LoginResponse usrSession, out Guid TokenId)
    {
        TokenId = Guid.NewGuid();

        IEnumerable<Claim> claims = new Claim[] {
            new Claim("UserId", usrSession.UserId.ToString()),
            new Claim("UserRole", usrSession.UserRole.ToString()),
            new Claim("UserName", usrSession.UserName),
            new Claim("Email", usrSession.Email),
            new Claim(JwtRegisteredClaimNames.Jti, TokenId.ToString()),
            new Claim(ClaimTypes.Role, usrSession.UserRole.ToString()),
            new Claim(ClaimTypes.NameIdentifier, usrSession.UserId.ToString())
            // ClaimTypes.Expiration removed — `expires:` below already sets the standard exp claim
        };
        return claims;
    }

    public async Task<JwtUserToken?> CreateJwtUserTokenAsync(ApplicationUser? user)
    {
        if (user == null)
            return null;

        var roles = await _userManager.GetRolesAsync(user);

        var _usrSession = new LoginResponse
        {
            UserId = Guid.Parse(user.Id),
            UserName = user.UserName,
            UserRole = roles.FirstOrDefault() ?? "User",
            Email = user.Email
        };

        if (_usrSession == null) throw new ArgumentException($"{nameof(_usrSession)} cannot be null");

        var _userToken = new JwtUserToken();
        Guid tokenId = Guid.Empty;

        //get the key from user-secrets and set token expiration time
        var key = System.Text.Encoding.ASCII.GetBytes(_jwtOptions.IssuerSigningKey);
        DateTime expireTime = DateTime.UtcNow.AddMinutes(_jwtOptions.LifeTimeMinutes);

        //generate the token, including my own defined claims, expiration time, signing credentials
        var JWToken = new JwtSecurityToken(issuer: _jwtOptions.ValidIssuer,
            audience: _jwtOptions.ValidAudience,
            claims: CreateClaims(_usrSession, out tokenId),
            notBefore: new DateTimeOffset(DateTime.UtcNow).DateTime,
            expires: expireTime,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));

        //generate a JWT user token with some unencrypted information as well
        _userToken.TokenId = tokenId;
        _userToken.EncryptedToken = new JwtSecurityTokenHandler().WriteToken(JWToken);
        _userToken.ExpireTime = expireTime;
        _userToken.UserRole = _usrSession.UserRole.ToString();
        _userToken.UserName = _usrSession.UserName;
        _userToken.UserId = _usrSession.UserId.Value;

        return _userToken;
    }


}