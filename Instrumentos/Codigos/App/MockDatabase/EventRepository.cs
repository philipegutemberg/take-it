using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Repositories;
using MockDatabase.Base;

namespace MockDatabase
{
    internal class EventRepository : MockBaseRepository<Event>, IEventRepository
    {
        public Task<Event> Register(Event newEvent)
        {
            Insert(newEvent.Id, newEvent);

            return Task.FromResult(newEvent);
        }

        public Task<Event?> GetById(string id)
        {
            return Task.FromResult(GetByKey(id));
        }

        public Task<IEnumerable<Event>> GetAllAvailable()
        {
            return Task.FromResult(GetAllEntities().Where(e => e.Available));
        }
    }
}