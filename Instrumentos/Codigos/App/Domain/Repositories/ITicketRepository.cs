using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Repositories
{
    public interface ITicketRepository
    {
        Task Save(Ticket ticket);

        Task<Ticket?> GetById(string ticketId);

        Task<IEnumerable<Ticket>> GetByIds(IEnumerable<string> ticketsIds);
    }
}