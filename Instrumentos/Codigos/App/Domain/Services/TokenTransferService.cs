using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.Models;
using Domain.Repositories;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    internal class TokenTransferService : ITokenTransferService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ITokenService _tokenService;

        public TokenTransferService(ITicketRepository ticketRepository, IEventRepository eventRepository, ITokenService tokenService)
        {
            _ticketRepository = ticketRepository;
            _eventRepository = eventRepository;
            _tokenService = tokenService;
        }
        
        public async Task Transfer(string username, string ticketId, string ticketOwnerAddress)
        {
            Ticket? ticket = await _ticketRepository.GetById(ticketId);
            if (ticket == null) throw new TicketNotFoundException(ticketId);

            Event? @event = await _eventRepository.GetById(ticket.EventId);
            if (@event == null) throw new EventNotFoundException(ticket.EventId);

            await _tokenService.TransferToTicketOwner(@event.TokenContractId!, ticketOwnerAddress, ticket.TokenId);
        }
    }
}