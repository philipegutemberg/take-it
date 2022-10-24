using System;

namespace Domain.Exceptions
{
    public class InvalidAddressException : Exception
    {
        public InvalidAddressException(string address)
            : base($"Invalid or inexistant address: {address}.")
        { }
    }
}