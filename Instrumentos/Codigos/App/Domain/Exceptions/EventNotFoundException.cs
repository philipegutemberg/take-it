using System;
using System.Security.Cryptography;

namespace Domain.Exceptions
{
    public class EventNotFoundException : Exception
    {
        public EventNotFoundException(string eventId)
            :base($"Event {eventId} not found.")
        {
        }
    }
}