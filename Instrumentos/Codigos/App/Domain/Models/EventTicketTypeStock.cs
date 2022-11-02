using Domain.Models.Users;

namespace Domain.Models
{
    public record EventTicketTypeStock
    {
        private readonly object _lockObj = new object();

        public EventTicketTypeStock(string eventTicketTypeCode, string eventCode, long availableTickets)
        {
            EventTicketTypeCode = eventTicketTypeCode;
            EventCode = eventCode;
            TotalAvailableTickets = availableTickets;
            CurrentlyAvailableTickets = TotalAvailableTickets;
        }

        public EventTicketTypeStock(string eventTicketTypeCode, string eventCode, long totalAvailableTickets, long currentlyAvailableTickets)
        {
            EventTicketTypeCode = eventTicketTypeCode;
            EventCode = eventCode;
            TotalAvailableTickets = totalAvailableTickets;
            CurrentlyAvailableTickets = currentlyAvailableTickets;
        }

        public string EventTicketTypeCode { get; }
        public string EventCode { get; }
        public long TotalAvailableTickets { get; }
        public long CurrentlyAvailableTickets { get; private set; }
        public bool OutOfStock => CurrentlyAvailableTickets == 0;

        public bool TryIssueTicket(CustomerUser customer, long tokenId, out Ticket? ticket)
        {
            lock (_lockObj)
            {
                if (!OutOfStock)
                {
                    --CurrentlyAvailableTickets;

                    ticket = new Ticket(EventCode, EventTicketTypeCode, customer, tokenId);

                    return true;
                }

                ticket = null;
                return false;
            }
        }
    }
}