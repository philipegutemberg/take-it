using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Users;
using Domain.Repositories;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    internal class TokenLogProcessingService : ITokenLogProcessingService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ICustomerRepository _customerRepository;

        public TokenLogProcessingService(ITicketRepository ticketRepository, ICustomerRepository customerRepository)
        {
            _ticketRepository = ticketRepository;
            _customerRepository = customerRepository;
        }

        public async Task ProcessEventLog(string fromAddress, string toAddress, long tokenId)
        {
            Ticket ticket = await _ticketRepository.GetByTokenId(tokenId);
            CustomerUser? customerTo = await _customerRepository.GetByInternalAddress(toAddress);
            /* If customerTo not found, it means ticket was transfer to an outside account.
             Then the ticket gets "ownerless" to our systems, and waits for final transfer back. */
            ticket.AssignOwner(customerTo?.Code);
            await _ticketRepository.UpdateOwner(ticket);
        }
    }
}