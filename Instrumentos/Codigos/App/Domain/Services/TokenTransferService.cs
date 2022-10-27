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
        private readonly IUserRepository<Customer> _customerRepository;

        public TokenTransferService(
            ITicketRepository ticketRepository,
            IEventRepository eventRepository,
            ITokenService tokenService,
            IUserRepository<Customer> customerRepository)
        {
            _ticketRepository = ticketRepository;
            _eventRepository = eventRepository;
            _tokenService = tokenService;
            _customerRepository = customerRepository;
        }

        public async Task Transfer(string username, string ticketCode)
        {
            Ticket ticket = await _ticketRepository.GetByCode(ticketCode);
            Event @event = await _eventRepository.GetByCode(ticket.EventCode);
            Customer customer = await _customerRepository.GetByUsername(username);

            await _tokenService.TransferToCustomer(ticket, @event, customer);
        }
    }
}