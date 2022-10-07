using System.Collections.Generic;
using Domain.Models.Users;

namespace Domain.Models
{
    public record EventTicketStock
    {
        private long _availableCount;
        
        public EventTicketStock(string eventId, long ticketsCount)
        {
            EventId = eventId;
            MaxCount = ticketsCount;
            AvailableCount = MaxCount;
            OutOfStock = false;
        }

        public EventTicketStock() { }

        public string EventId { get; init; }
        public long MaxCount { get; init; }
        public long AvailableCount
        {
            get => _availableCount;
            init => _availableCount = value;
        }
        public bool OutOfStock { get; private set; }

        public bool TryIssueTicket(Customer customer, out Ticket? ticket)
        {
            if (!OutOfStock)
            {
                --_availableCount;
                UpdateOutOfStock();
                
                ticket = new Ticket(EventId);
                ticket.Purchase(customer.Id);
                customer.AssignTicket(ticket.Id);

                return true;
            }

            ticket = null;
            return false;
        }

        private void UpdateOutOfStock()
        {
            OutOfStock = AvailableCount == 0;
        }
    }
}