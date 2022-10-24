using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Repositories;
using MockDatabase.Base;

namespace MockDatabase
{
    internal class TicketRepository : MockBaseRepository<Ticket>, ITicketRepository
    {
        public Task Save(Ticket ticket)
        {
            Insert(ticket.Code, ticket);

            return Task.CompletedTask;
        }

        public Task<Ticket?> GetById(string ticketId) => Task.FromResult(GetByKey(ticketId));

        public Task<IEnumerable<Ticket>> GetByIds(IEnumerable<string> ticketsIds)
        {
            return Task.FromResult(ticketsIds.Select(tid => Storage[tid]));
        }
    }
}