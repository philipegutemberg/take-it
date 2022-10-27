using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Repositories
{
    public interface IEventTicketTypeRepository
    {
        Task<EventTicketType> Save(EventTicketType eventTicketType);

        Task<EventTicketType> GetByCode(string code);

        Task<IEnumerable<EventTicketType>> GetAllAvailableFromEvent(Event @event);
    }
}