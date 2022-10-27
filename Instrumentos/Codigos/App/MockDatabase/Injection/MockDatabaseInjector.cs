using Domain.Models.Users;
using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MockDatabase.Injection
{
    public static class MockDatabaseInjector
    {
        public static IServiceCollection InjectMockDatabaseServices(this IServiceCollection services) => services
            .AddSingleton<IUserRepository<User>, UserRepository>()
            .AddSingleton<IUserRepository<Customer>, CustomerRepository>()
            .AddSingleton<IEventRepository, EventRepository>()
            .AddSingleton<ITicketRepository, TicketRepository>()
            .AddSingleton<IEventTicketTypeRepository, EventTicketTypeRepository>();
    }
}