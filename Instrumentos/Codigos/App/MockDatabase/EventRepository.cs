using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.Models;
using Domain.Repositories;
using MockDatabase.Base;

namespace MockDatabase
{
    internal class EventRepository : MockBaseRepository<Event>, IEventRepository
    {
        public Task<Event> Save(Event newEvent)
        {
            Insert(newEvent.Code, newEvent);

            return Task.FromResult(newEvent);
        }

        public Task<Event> GetByCode(string code)
        {
            var @event = GetByKey(code);
            if (@event == null) throw new EventNotFoundException(code);

            return Task.FromResult(@event);
        }

        public Task<IEnumerable<Event>> GetAllEnabled()
        {
            return Task.FromResult(GetAllEntities());
        }
    }
}