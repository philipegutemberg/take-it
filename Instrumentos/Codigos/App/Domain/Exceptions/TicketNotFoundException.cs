using System;

namespace Domain.Exceptions
{
    public class TicketNotFoundException : Exception
    {
        public TicketNotFoundException(string ticketId)
            :base($"Ticket {ticketId} not found.")
        {
            
        }
    }
}