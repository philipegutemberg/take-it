using Domain.Services;
using Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Injection
{
    public static class DomainServicesInjector
    {
        public static IServiceCollection InjectDomainServices(this IServiceCollection services) => services
            .AddTransient<IEventService, EventService>()
            .AddTransient<ITicketBuyService, TicketBuyService>()
            .AddTransient<IUserService, UserService>();
    }
}