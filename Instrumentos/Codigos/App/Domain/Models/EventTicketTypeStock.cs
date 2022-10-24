using System.Diagnostics.Tracing;
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
            OutOfStock = false;
        }

        public string EventTicketTypeCode { get; }
        public string EventCode { get; }
        public long TotalAvailableTickets { get; }
        public long CurrentlyAvailableTickets { get; private set; }
        public bool OutOfStock { get; private set; }

        public bool TryIssueTicket(Customer customer, long tokenId, out Ticket? ticket)
        {
            /* ToDo: Gerenciar reserva de ingressos por um tempo até que a compra seja efetivada e o token emitido no blockchain */
            lock (_lockObj)
            {
                if (!OutOfStock)
                {
                    --CurrentlyAvailableTickets;
                    UpdateOutOfStock();

                    ticket = new Ticket(EventCode, EventTicketTypeCode, customer, tokenId);

                    return true;
                }

                ticket = null;
                return false;
            }
        }

        private void UpdateOutOfStock()
        {
            OutOfStock = CurrentlyAvailableTickets == 0;
        }
    }
}