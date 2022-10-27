using System;

namespace Domain.Exceptions
{
    public class TicketNotFoundException : Exception
    {
        public TicketNotFoundException(string code)
            : base($"Ticket {code} not found.")
        {
        }
    }
}