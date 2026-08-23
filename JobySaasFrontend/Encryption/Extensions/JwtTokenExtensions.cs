using System.Text;
using JobySaasFrontend.Encryption.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace JobySaasFrontend.Encryption.Extensions;

// JwtTokenExtensions.cs — add JWT Bearer WITHOUT re-calling AddAuthentication
public static class JwtTokenExtension
{
    public static IServiceCollection AddJwtTokenService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.Position));

        var jwtOptions = configuration.GetSection(JwtOptions.Position).Get<JwtOptions>();
        if (jwtOptions == null)
            throw new InvalidOperationException("JwtConfig section is missing");

        services.AddScoped<JWTService>();

        services.AddAuthentication() // no scheme argument — doesn't touch the default
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = jwtOptions.ValidateIssuer,
                    ValidateAudience = jwtOptions.ValidateAudience,
                    ValidateLifetime = jwtOptions.ValidateLifetime,
                    ValidateIssuerSigningKey = jwtOptions.ValidateIssuerSigningKey,
                    ValidIssuer = jwtOptions.ValidIssuer,
                    ValidAudience = jwtOptions.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.IssuerSigningKey)) // also fixed ASCII→UTF8, same issue as JWTService.cs earlier
                };
            });

        return services;
    }
}