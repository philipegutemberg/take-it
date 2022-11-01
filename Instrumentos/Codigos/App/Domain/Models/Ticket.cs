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
            OwnerCustomerCode = ownerCustomer.Code;
            TokenId = tokenId;
            UsedOnEvent = false;
        }

        public Ticket(string code, string eventCode, string eventTicketTypeCode, DateTime purchaseDate, string? ownerCustomerCode, long tokenId, bool usedOnEvent)
        {
            Code = code;
            EventCode = eventCode;
            EventTicketTypeCode = eventTicketTypeCode;
            PurchaseDate = purchaseDate;
            OwnerCustomerCode = ownerCustomerCode;
            TokenId = tokenId;
            UsedOnEvent = usedOnEvent;
        }

        public string Code { get; }
        public string EventCode { get; }
        public string EventTicketTypeCode { get; }
        public DateTime PurchaseDate { get; }
        public string? OwnerCustomerCode { get; private set; }
        public long TokenId { get; }
        public bool UsedOnEvent { get; private set; }

        public bool HasCurrentCustomerOwner => OwnerCustomerCode == null;

        public void AssignOwner(string? ownerCustomerCode)
        {
            OwnerCustomerCode = ownerCustomerCode;
        }

        public bool TryMarkAsUsed()
        {
            if (UsedOnEvent)
                return false;

            UsedOnEvent = true;
            return true;
        }

        public bool TryUnmarkAsUsed()
        {
            if (!UsedOnEvent)
                return false;

            UsedOnEvent = false;
            return true;
        }
    }
}