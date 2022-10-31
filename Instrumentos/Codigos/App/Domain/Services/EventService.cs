using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Repositories;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    internal class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEventTicketTypeRepository _eventTicketTypeRepository;
        private readonly ITokenCreationService _tokenCreationService;
        private readonly ITokenMetadataService _tokenMetadataService;

        public EventService(
            IEventRepository eventRepository,
            IEventTicketTypeRepository eventTicketTypeRepository,
            ITokenCreationService tokenCreationService,
            ITokenMetadataService tokenMetadataService)
        {
            _eventRepository = eventRepository;
            _eventTicketTypeRepository = eventTicketTypeRepository;
            _tokenCreationService = tokenCreationService;
            _tokenMetadataService = tokenMetadataService;
        }

        public async Task Register(Event newEvent, List<EventTicketType> ticketTypes)
        {
            string tokenContractId = await _tokenCreationService.Create(newEvent);
            newEvent.AssignTokenContractAddress(tokenContractId);

            await _eventRepository.Insert(newEvent);

            var tasks = ticketTypes.Select(tt => SaveEventTicketType(newEvent, tt));
            await Task.WhenAll(tasks);
        }

        public async Task<IDictionary<Event, EventTicketType[]>> ListAllAvailable()
        {
            var response = new Dictionary<Event, EventTicketType[]>();

            var events = await _eventRepository.GetAllEnabled();

            foreach (var @event in events)
            {
                var eventTypes = await _eventTicketTypeRepository.GetAllByEvent(@event.Code);

                eventTypes = eventTypes.Where(e => e.Available);

                var eventTicketTypes = eventTypes as EventTicketType[] ?? eventTypes.ToArray();
                if (eventTicketTypes.Any())
                    response.Add(@event, eventTicketTypes);
            }

            return response;
        }

        private async Task SaveEventTicketType(Event @event, EventTicketType eventTicketType)
        {
            var metadataFileLink = await _tokenMetadataService.BuildAndGenerateLink(@event, eventTicketType);

            eventTicketType.AssignMetadataFileUrl(metadataFileLink);

            await _eventTicketTypeRepository.Insert(eventTicketType);
        }
    }
}