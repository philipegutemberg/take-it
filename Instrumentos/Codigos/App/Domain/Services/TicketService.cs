using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Users;
using Domain.Repositories;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    internal class TicketService : ITicketService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IEventTicketTypeRepository _eventTicketTypeRepository;

        public TicketService(
            ICustomerRepository customerRepository,
            ITicketRepository ticketRepository,
            IEventRepository eventRepository,
            IEventTicketTypeRepository eventTicketTypeRepository)
        {
            _customerRepository = customerRepository;
            _ticketRepository = ticketRepository;
            _eventRepository = eventRepository;
            _eventTicketTypeRepository = eventTicketTypeRepository;
        }

        public async Task<IEnumerable<(Ticket Ticket, Event @Event, EventTicketType EventTicketType)>> ListMyTickets(string username)
        {
            CustomerUser customer = await _customerRepository.GetByUsername(username);

            var tickets = await _ticketRepository.GetAllOwnedByCustomer(customer.Code);

            var events = await GetEvents(tickets);
            var eventTicketTypes = await GetEventTicketTypes(tickets);

            return tickets.Select(ticket =>
            {
                var @event = events.FirstOrDefault(e => e.Code == ticket.EventCode)!;
                var eventTicketType = eventTicketTypes.FirstOrDefault(ett => ett.Code == ticket.EventTicketTypeCode)!;

                return (ticket, @event, eventTicketType);
            });
        }

        private async Task<Event[]> GetEvents(IEnumerable<Ticket> tickets)
        {
            var eventCodes = tickets.Select(t => t.EventCode).Distinct();

            var tasks = eventCodes.Select(ec => _eventRepository.GetByCode(ec));

            return await Task.WhenAll(tasks);
        }

        private async Task<EventTicketType[]> GetEventTicketTypes(IEnumerable<Ticket> tickets)
        {
            var eventTicketTypeCodes = tickets.Select(t => t.EventTicketTypeCode).Distinct();

            var tasks = eventTicketTypeCodes.Select(ettc => _eventTicketTypeRepository.GetByCode(ettc));

            return await Task.WhenAll(tasks);
        }
    }
}