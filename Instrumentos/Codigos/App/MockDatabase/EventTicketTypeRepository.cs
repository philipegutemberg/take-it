using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.Models;
using Domain.Repositories;
using MockDatabase.Base;

namespace MockDatabase
{
    internal class EventTicketTypeRepository : MockBaseRepository<EventTicketType>, IEventTicketTypeRepository
    {
        public Task<EventTicketType> Save(EventTicketType eventTicketType)
        {
            Insert(eventTicketType.Code, eventTicketType);

            return Task.FromResult(eventTicketType);
        }

        public Task<EventTicketType> GetByCode(string code)
        {
            var ticketType = GetByKey(code);
            if (ticketType == null)
                throw new EventTicketTypeNotFoundException(code);

            return Task.FromResult(ticketType);
        }

        public Task<IEnumerable<EventTicketType>> GetAllAvailableFromEvent(Event @event)
        {
            return Task.FromResult((IEnumerable<EventTicketType>)Storage.Where(s => s.Value.Available && s.Value.EventCode == @event.Code));
        }
    }
}