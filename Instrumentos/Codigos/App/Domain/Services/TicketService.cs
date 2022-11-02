using System.Collections.Generic;
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

        public TicketService(ICustomerRepository customerRepository, ITicketRepository ticketRepository)
        {
            _customerRepository = customerRepository;
            _ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<Ticket>> ListMyTickets(string username)
        {
            CustomerUser customer = await _customerRepository.GetByUsername(username);

            return await _ticketRepository.GetAllOwnedByCustomer(customer.Code);
        }
    }
}