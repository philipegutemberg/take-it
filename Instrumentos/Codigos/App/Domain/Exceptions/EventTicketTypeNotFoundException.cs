using System;

namespace Domain.Exceptions
{
    public class EventTicketTypeNotFoundException : Exception
    {
        public EventTicketTypeNotFoundException(string code)
         : base($"Ticket type not found: {code}")
        {
        }
    }
}