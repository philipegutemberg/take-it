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
            .AddTransient<IGenericUserService<GenericUser>, GenericUserService>()
            .AddTransient<ICustomerService, CustomerService>()
            .AddTransient<ITokenLogProcessingService, TokenLogProcessingService>()
            .AddTransient<ISpecialUserService, SpecialUserService>();
    }
}