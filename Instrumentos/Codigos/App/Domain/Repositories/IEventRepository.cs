using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Repositories
{
    public interface IEventRepository
    {
        Task<Event> Register(Event newEvent);

        Task<Event?> GetById(string id);

        Task<IEnumerable<Event>> GetAllAvailable();
    }
}