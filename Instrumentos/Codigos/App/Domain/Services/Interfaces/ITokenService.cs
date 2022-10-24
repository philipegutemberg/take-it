using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Users;

namespace Domain.Services.Interfaces
{
    public interface ITokenService
    {
        Task EmitToken(EventTicketType ticketType, Ticket ticket, Event @event, Customer customer);

        Task TransferToCustomer(Ticket ticket, Event @event, Customer customer);

        Task<long> GetCustomerBalance(Event @event, Customer customer);
    }
}