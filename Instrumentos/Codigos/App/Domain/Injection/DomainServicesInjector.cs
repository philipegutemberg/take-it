using Domain.Models.Users;
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
            .AddTransient<ITicketService, TicketService>()
            .AddTransient<ITokenTransferService, TokenTransferService>()
            .AddTransient<ITicketValidationService, TicketValidationService>()
            .AddTransient<IUserService<User>, UserService<User>>()
            .AddTransient<ICustomerService, CustomerService>()
            .AddTransient<ITokenLogProcessingService, TokenLogProcessingService>();
    }
}