using System;

namespace Domain.Exceptions
{
    public class EventNotFoundException : Exception
    {
        public EventNotFoundException(string eventId)
            : base($"Event {eventId} not found.")
        {
        }
    }
}