using JobySaasFrontend.Encryption.Options;

namespace JobySaasFrontend.Encryption.Extensions;

public static class EncryptionExtensions
{
    public static IServiceCollection AddEncryptions(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<AesEncryptionOptions>(
            options => configuration.GetSection(AesEncryptionOptions.Position).Bind(options));
        serviceCollection.AddTransient<Encryptions>();

        return serviceCollection;
    }
}