using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Users;

namespace Domain.Services.Interfaces
{
    public interface ITokenService
    {
        Task EmitToken(EventTicketType ticketType, Ticket ticket, Event @event, CustomerUser customer);

        Task TransferToCustomer(Ticket ticket, Event @event, CustomerUser customer);

        Task<long> GetCustomerBalance(Event @event, CustomerUser customer);

        Task<string> GetCustomerInternalAddress(CustomerUser customer);

        Task<bool> CheckCustomerTokenOwnership(Event @event, CustomerUser customer, Ticket ticket);
    }
}