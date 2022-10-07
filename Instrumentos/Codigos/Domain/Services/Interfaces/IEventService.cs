using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface IEventService
    {
        Task Register(Event newEvent);

        Task<IEnumerable<Event>> ListAllAvailable();
    }
}