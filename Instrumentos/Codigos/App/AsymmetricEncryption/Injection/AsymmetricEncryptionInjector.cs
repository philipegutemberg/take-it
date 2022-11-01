using AsymmetricEncryption.Keys;
using Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AsymmetricEncryption.Injection
{
    public static class AsymmetricEncryptionInjector
    {
        public static IServiceCollection InjectAsymmetricEncryptionServices(this IServiceCollection services, string pemFilePath) => services
            .AddSingleton(p => new KeysStore(pemFilePath))
            .AddTransient<IEncryptionService, AsymmetricEncryptionService>();
    }
}