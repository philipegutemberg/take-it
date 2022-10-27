using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Repositories
{
    public interface IEventRepository
    {
        Task<Event> Save(Event newEvent);

        Task<Event> GetByCode(string code);

        Task<IEnumerable<Event>> GetAllEnabled();
    }
}