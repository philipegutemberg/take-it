using Database.SQLServer.Connection;
using Domain.Models.Users;
using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Database.SQLServer.Injection
{
    public static class SqlServerInjector
    {
        public static IServiceCollection InjectSqlServerServices(this IServiceCollection services) => services
            .AddTransient<DbConnectionFactory>()
            .AddTransient<DbConnection>()
            .AddTransient<ICustomerRepository, CustomerRepository>()
            .AddTransient<IBackofficeUserRepository, BackofficeUserRepository>()
            .AddTransient<IGatekeeperUserRepository, GatekeeperUserRepository>()
            .AddTransient<IGenericUserRepository<GenericUser>, GenericUserRepository>()
            .AddTransient<IEventRepository, EventRepository>()
            .AddTransient<IEventTicketTypeRepository, EventTicketTypeRepository>()
            .AddTransient<EventTicketTypeRepository>()
            .AddTransient<ITicketRepository, TicketRepository>()
            .AddTransient<TicketRepository>()
            .AddTransient<ITokenEventProcessingRepository, TokenEventProcessingRepository>();
    }
}