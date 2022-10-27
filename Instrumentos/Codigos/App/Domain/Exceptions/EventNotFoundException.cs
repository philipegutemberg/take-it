using System;

namespace Domain.Exceptions
{
    public class EventNotFoundException : Exception
    {
        public EventNotFoundException(string code)
            : base($"Event {code} not found.")
        {
        }
    }
}