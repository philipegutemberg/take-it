using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.Users;
using Domain.Repositories;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    internal class TicketService : ITicketService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITicketRepository _ticketRepository;

        public TicketService(IUserRepository userRepository, ITicketRepository ticketRepository)
        {
            _userRepository = userRepository;
            _ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<Ticket>> ListMyTickets(string username)
        {
            Customer customer = await _userRepository.GetCustomer(username);

            return await _ticketRepository.GetByIds(customer.TicketsCodes);
        }
    }
}