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
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly ITokenService _tokenService;

        public TicketBuyService(IUserRepository userRepository, IEventRepository eventRepository, ITicketRepository ticketRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _ticketRepository = ticketRepository;
            _tokenService = tokenService;
        }

        public async Task Buy(string username, string eventId)
        {
            User? user = await _userRepository.GetUser(username);
            if (user == null) throw new UserNotFoundException(username);

            Customer customer = (Customer)user;

            Event? @event = await _eventRepository.GetById(eventId);
            if (@event == null) throw new EventNotFoundException(eventId);

            if (@event.TryPurchase(customer, out Ticket? ticket))
            {
                await _ticketRepository.Save(ticket!);
                await _tokenService.EmitToken(ticket, @event, customer);
            }
            else
            {
                throw new UnsuccessfulPurchaseException();
            }
        }
    }
}