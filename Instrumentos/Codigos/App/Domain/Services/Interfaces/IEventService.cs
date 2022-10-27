using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface IEventService
    {
        Task Register(Event newEvent, List<EventTicketType> ticketTypes);

        Task<IDictionary<Event, EventTicketType[]>> ListAllAvailable();
    }
}