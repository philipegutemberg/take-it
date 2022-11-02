using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.Users;
using Domain.Repositories;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    internal class TicketBuyService : ITicketBuyService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly ITokenService _tokenService;
        private readonly IEventTicketTypeRepository _eventTicketTypeRepository;

        public TicketBuyService(
            ICustomerRepository customerRepository,
            IEventRepository eventRepository,
            ITicketRepository ticketRepository,
            ITokenService tokenService,
            IEventTicketTypeRepository eventTicketTypeRepository)
        {
            _customerRepository = customerRepository;
            _eventRepository = eventRepository;
            _ticketRepository = ticketRepository;
            _tokenService = tokenService;
            _eventTicketTypeRepository = eventTicketTypeRepository;
        }

        public async Task Buy(string username, string eventTicketTypeCode)
        {
            CustomerUser customer = await _customerRepository.GetByUsername(username);
            EventTicketType eventTicketType = await _eventTicketTypeRepository.GetByCode(eventTicketTypeCode);
            Event @event = await _eventRepository.GetByCode(eventTicketType.EventCode);

            if (@event.TryIssueTicket(customer, eventTicketType, out Ticket? ticket))
            {
                await _ticketRepository.Insert(ticket!);
                await _tokenService.EmitToken(eventTicketType, ticket!, @event, customer);
                await _eventRepository.UpdateIssuedTickets(@event.Code, @event.AlreadyIssuedTickets);
            }
            else
            {
                throw new UnsuccessfulPurchaseException();
            }
        }
    }
}