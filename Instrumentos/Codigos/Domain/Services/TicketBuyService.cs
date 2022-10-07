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

        public TicketBuyService(IUserRepository userRepository, IEventRepository eventRepository, ITicketRepository ticketRepository)
        {
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _ticketRepository = ticketRepository;
        }
        
        public async Task Buy(string username, string eventId)
        {
            User? user = await _userRepository.GetUser(username);
            if (user == null)
                throw new UserNotFoundException(username);

            Customer customer = (Customer)user;

            Event? @event = await _eventRepository.GetById(eventId);
            if (@event == null)
                throw new EventNotFoundException(eventId);

            if (@event.TryPurchase(customer, out Ticket? ticket))
            {
                await _ticketRepository.Save(ticket);
            }
            else
                throw new UnsuccessfulPurchaseException();
        }
    }
}