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
        private readonly ITokenCreationService _tokenCreationService;

        public EventService(IEventRepository eventRepository, ITokenCreationService tokenCreationService)
        {
            _eventRepository = eventRepository;
            _tokenCreationService = tokenCreationService;
        }

        public async Task Register(Event newEvent)
        {
            string tokenContractId = await _tokenCreationService.Create(newEvent);
            newEvent.AssignTokenContractAddress(tokenContractId);

            await _eventRepository.Register(newEvent);
        }
        
        /* ToDo: CREATE TICKET TYPE HERE !!!!!!!!!!!! */

        public async Task<IEnumerable<Event>> ListAllAvailable()
        {
            return await _eventRepository.GetAllAvailable();
        }
    }
}