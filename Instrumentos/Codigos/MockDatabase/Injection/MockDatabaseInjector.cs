using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MockDatabase.Injection
{
    public static class MockDatabaseInjector
    {
        public static IServiceCollection InjectMockDatabaseServices(this IServiceCollection services) => services
            .AddSingleton<IUserRepository, UserRepository>()
            .AddSingleton<IEventRepository, EventRepository>()
            .AddSingleton<ITicketRepository, TicketRepository>();
    }
}