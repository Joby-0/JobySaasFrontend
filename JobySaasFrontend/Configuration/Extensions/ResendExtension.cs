using Configuration.Options;
using Resend;

namespace JobySaasFrontend.Configuration.Extension;

public static class ResendExtensions
{
    public static IServiceCollection AddResendOwn(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        
        var apiKey = configuration["Resend:ApiKey"]
            ?? throw new InvalidOperationException(
                "Resend API key is missing.");;
        serviceCollection.AddResend(apiKey);

        serviceCollection.AddHttpClient<ResendClient>(client =>
        {
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    apiKey);
        });

        return serviceCollection;
    }
}