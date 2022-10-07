using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.Users;
using Domain.Repositories;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITicketRepository _ticketRepository;

        public UserService(IUserRepository userRepository, ITicketRepository ticketRepository)
        {
            _userRepository = userRepository;
            _ticketRepository = ticketRepository;
        }
        
        public async Task<IEnumerable<Ticket>> ListMyTickets(string username)
        {
            User? user = await _userRepository.GetUser(username);
            if (user == null)
                throw new UserNotFoundException(username);

            Customer customer = (Customer)user;

            return await _ticketRepository.GetByIds(customer.TicketsIds);
        }
    }
}