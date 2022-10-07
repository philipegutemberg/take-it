using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Repositories;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    internal class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        
        public async Task Register(Event newEvent)
        {
            await _eventRepository.Register(newEvent);
        }

        public async Task<IEnumerable<Event>> ListAllAvailable()
        {
            return await _eventRepository.GetAllAvailable();
        }
    }
}