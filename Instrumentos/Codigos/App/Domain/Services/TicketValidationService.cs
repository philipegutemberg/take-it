using System;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Users;
using Domain.Repositories;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    internal class TicketValidationService : ITicketValidationService
    {
        private readonly IQRCodeService _qrCodeService;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ITokenService _tokenService;
        private readonly IEncryptionService _encryptionService;

        public TicketValidationService(
            IQRCodeService qrCodeService,
            ICustomerRepository customerRepository,
            ITicketRepository ticketRepository,
            IEventRepository eventRepository,
            ITokenService tokenService,
            IEncryptionService encryptionService)
        {
            _qrCodeService = qrCodeService;
            _customerRepository = customerRepository;
            _ticketRepository = ticketRepository;
            _eventRepository = eventRepository;
            _tokenService = tokenService;
            _encryptionService = encryptionService;
        }

        public async Task<byte[]> GetTicketImage(string username, string ticketCode)
        {
            Ticket ticket = await _ticketRepository.GetByCode(ticketCode);
            if (ticket.UsedOnEvent)
                return Array.Empty<byte>();

            Event @event = await _eventRepository.GetByCode(ticket.EventCode);
            CustomerUser customer = await _customerRepository.GetByUsername(username);
            if (ticket.OwnerCustomerCode != customer.Code)
                return Array.Empty<byte>();

            bool isCustomerOwner = await _tokenService.CheckCustomerTokenOwnership(@event, customer, ticket);
            if (!isCustomerOwner)
                return Array.Empty<byte>();

            string qrCodeText = _encryptionService.Encrypt($"{customer.Code}|{ticket.Code}");

            return await _qrCodeService.Generate(qrCodeText);
        }

        public async Task<bool> IsValid(string qrCodeText)
        {
            qrCodeText = _encryptionService.Decrypt(qrCodeText);
            var splittedText = qrCodeText.Split('|');
            string customerCode = splittedText[0];
            string ticketCode = splittedText[1];

            Ticket ticket = await _ticketRepository.GetByCode(ticketCode);
            if (ticket.UsedOnEvent)
                return false;
            if (ticket.OwnerCustomerCode != customerCode)
                return false;

            Event @event = await _eventRepository.GetByCode(ticket.EventCode);
            CustomerUser customer = await _customerRepository.GetByCode(customerCode);

            if (ticket.TryMarkAsUsed())
                await _ticketRepository.UpdateUsedOnEvent(ticket);

            bool isCustomerOwner = await _tokenService.CheckCustomerTokenOwnership(@event, customer, ticket);
            if (!isCustomerOwner && ticket.TryUnmarkAsUsed())
                await _ticketRepository.UpdateUsedOnEvent(ticket);

            return isCustomerOwner;
        }
    }
}