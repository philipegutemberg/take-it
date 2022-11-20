using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<(Ticket Ticket, Event @Event, EventTicketType EventTicketType)>> ListMyTickets(string username);
    }
}