using System;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Users;
using Domain.Repositories;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    internal class TokenTransferService : ITokenTransferService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ITokenService _tokenService;
        private readonly ICustomerRepository _customerRepository;

        public TokenTransferService(
            ITicketRepository ticketRepository,
            IEventRepository eventRepository,
            ITokenService tokenService,
            ICustomerRepository customerRepository)
        {
            _ticketRepository = ticketRepository;
            _eventRepository = eventRepository;
            _tokenService = tokenService;
            _customerRepository = customerRepository;
        }

        public async Task Transfer(string username, string ticketCode)
        {
            Ticket ticket = await _ticketRepository.GetByCode(ticketCode);
            if (ticket.UsedOnEvent)
                throw new Exception("Ticket already used.");

            Event @event = await _eventRepository.GetByCode(ticket.EventCode);
            CustomerUser customer = await _customerRepository.GetByUsername(username);

            ticket.AssignOwner(null);
            await _ticketRepository.UpdateOwner(ticket);

            await _tokenService.TransferToCustomer(ticket, @event, customer);
        }
    }
}