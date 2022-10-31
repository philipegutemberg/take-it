using System;
using Domain.Enums;
using Domain.Models.Users;

namespace Domain.Models
{
    public class EventTicketType
    {
        public EventTicketType(
            Event @event,
            string ticketName,
            DateTime startDate,
            DateTime endDate,
            EnumTicketQualification qualification,
            decimal priceBrl,
            long availableTickets)
        {
            Code = Guid.NewGuid().ToString();
            EventCode = @event.Code;
            TicketName = ticketName;
            StartDate = startDate;
            EndDate = endDate;
            Qualification = qualification;
            PriceBrl = priceBrl;
            MetadataFileUrl = string.Empty;
            TicketStock = new EventTicketTypeStock(Code, EventCode, availableTickets);

            @event.AssignTicketType(Code);
        }

        public EventTicketType(
            string code,
            string eventCode,
            string ticketName,
            DateTime startDate,
            DateTime endDate,
            EnumTicketQualification qualification,
            decimal priceBrl,
            string metadataFileUrl,
            EventTicketTypeStock ticketStock)
        {
            Code = code;
            EventCode = eventCode;
            TicketName = ticketName;
            StartDate = startDate;
            EndDate = endDate;
            Qualification = qualification;
            PriceBrl = priceBrl;
            MetadataFileUrl = metadataFileUrl;
            TicketStock = ticketStock;
        }

        public string Code { get; }
        public string EventCode { get; }
        public string TicketName { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public bool SingleDay => StartDate == EndDate;
        public EnumTicketQualification Qualification { get; }
        public decimal PriceBrl { get; }
        public string MetadataFileUrl { get; private set; }
        public EventTicketTypeStock TicketStock { get; }
        public bool Available => EndDate >= DateTime.Today && !TicketStock.OutOfStock;

        public bool TryIssueTicket(Customer customer, long tokenId, out Ticket? ticket)
        {
            return TicketStock.TryIssueTicket(customer, tokenId, out ticket);
        }

        public void AssignMetadataFileUrl(string url)
        {
            if (!string.IsNullOrEmpty(MetadataFileUrl))
                throw new Exception("Metadata file url was already assigned.");

            MetadataFileUrl = url;
        }
    }
}