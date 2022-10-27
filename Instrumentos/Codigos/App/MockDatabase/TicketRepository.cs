using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions;
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

        public Task<Ticket> GetByCode(string code)
        {
            var ticket = GetByKey(code);
            if (ticket == null) throw new TicketNotFoundException(code);

            return Task.FromResult(ticket);
        }

        public Task<IEnumerable<Ticket>> GetByCodes(IEnumerable<string> ticketsCodes)
        {
            return Task.FromResult(ticketsCodes.Select(tid => Storage[tid]));
        }
    }
}