using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Repositories
{
    public interface IEventRepository
    {
        Task<Event> Insert(Event newEvent);

        Task<Event> GetByCode(string code);

        Task<IEnumerable<Event>> GetAllEnabled();

        Task UpdateIssuedTickets(string eventCode, long alreadyIssuedTickets);
    }
}