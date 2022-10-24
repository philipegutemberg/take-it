using System;
using Domain.Enums;
using Domain.Models.Users;

namespace Domain.Models
{
    public class EventTicketType
    {
        public EventTicketType(
            Event @event,
            DateTime startDate,
            DateTime endDate,
            EnumTicketQualification qualification,
            decimal priceBrl,
            string access,
            string metadataFileUrl,
            long availableTickets)
        {
            Code = Guid.NewGuid().ToString();
            EventCode = @event.Code;
            StartDate = startDate;
            EndDate = endDate;
            Qualification = qualification;
            PriceBrl = priceBrl;
            Access = access;
            MetadataFileUrl = metadataFileUrl;
            TicketStock = new EventTicketTypeStock(Code, EventCode, availableTickets);

            @event.AssignTicketType(Code);
        }

        public EventTicketType(
            string code,
            string eventCode,
            DateTime startDate,
            DateTime endDate,
            EnumTicketQualification qualification,
            decimal priceBrl,
            string access,
            string metadataFileUrl,
            EventTicketTypeStock ticketStock)
        {
            Code = code;
            EventCode = eventCode;
            StartDate = startDate;
            EndDate = endDate;
            Qualification = qualification;
            PriceBrl = priceBrl;
            Access = access;
            MetadataFileUrl = metadataFileUrl;
            TicketStock = ticketStock;
        }

        /* ToDo: Implementar regra para quando o organizador quer parar de vender o tipo, mesmo sem terem "acabado" os ingressos */

        public string Code { get; }
        public string EventCode { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public bool SingleDay => StartDate == EndDate;
        public EnumTicketQualification Qualification { get; }
        public decimal PriceBrl { get; }
        public string Access { get; }
        public string MetadataFileUrl { get; }
        public EventTicketTypeStock TicketStock { get; }

        public bool TryIssueTicket(Customer customer, long tokenId, out Ticket? ticket)
        {
            return TicketStock.TryIssueTicket(customer, tokenId, out ticket);
        }
    }
}