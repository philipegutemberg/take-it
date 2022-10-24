using System;
using Domain.Models.Users;

namespace Domain.Models
{
    public record Ticket
    {
        public Ticket(string eventCode, string eventTicketTypeCode, Customer ownerCustomer, long tokenId)
        {
            Code = Guid.NewGuid().ToString();
            EventCode = eventCode;
            EventTicketTypeCode = eventTicketTypeCode;
            PurchaseDate = DateTime.UtcNow;
            OwnerUserCode = ownerCustomer.Code;
            TokenId = tokenId;

            ownerCustomer.AssignTicket(Code);
        }

        public Ticket(string code, string eventCode, string eventTicketTypeCode, DateTime purchaseDate, string ownerUserCode, long tokenId)
        {
            Code = code;
            EventCode = eventCode;
            EventTicketTypeCode = eventTicketTypeCode;
            PurchaseDate = purchaseDate;
            OwnerUserCode = ownerUserCode;
            TokenId = tokenId;
        }

        public string Code { get; }
        public string EventCode { get; }
        public string EventTicketTypeCode { get; }
        public DateTime PurchaseDate { get; }
        public string OwnerUserCode { get; }
        public long TokenId { get; }
    }
}