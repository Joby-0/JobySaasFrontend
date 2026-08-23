using JobySaasFrontend.Encryption;
using JobySaasFrontend.Services;

namespace JobySaasFrontend.Configuration.Extension;

public static class ApiClientExtensions
{
    public static IServiceCollection AddApiClients(this IServiceCollection services, IConfiguration configuration)
    {
        var apiBaseUrl = configuration["Api:BaseUrl"] ?? throw new InvalidOperationException("Api:BaseUrl is missing from configuration.");

        services.AddTransient<JwtAuthHandler>();
        
        services.AddApiClient<IAuthApiClient, AuthApiClient>(apiBaseUrl);
        services.AddApiClient<IOrganizationApiClient, OrganizationApiClient>(apiBaseUrl);
        services.AddApiClient<ISubscriptionApiClient, SubscriptionApiClient>(apiBaseUrl);
        services.AddApiClient<IYoutubeApiClient, YoutubeApiClient>(apiBaseUrl);
        services.AddApiClient<IInvitationApiClient, InvitationApiClient>(apiBaseUrl);

        return services;
    }

    private static IServiceCollection AddApiClient<TInterface, TImplementation>(this IServiceCollection services, string baseUrl)
    where TInterface : class
    where TImplementation : class, TInterface
    {
        services.AddHttpClient<TInterface, TImplementation>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
        })
        .AddHttpMessageHandler<JwtAuthHandler>();

        return services;
    }
}