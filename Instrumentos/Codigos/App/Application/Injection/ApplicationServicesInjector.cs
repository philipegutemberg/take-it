using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Injection
{
    public static class ApplicationServicesInjector
    {
        public static IServiceCollection InjectApplicationServices(this IServiceCollection services) => services
            .AddTransient<TokenService>()
            .AddTransient<LoginService>()
            .AddTransient<HashService>();
    }
}