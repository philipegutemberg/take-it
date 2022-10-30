using System;

namespace Domain.Exceptions
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string message)
            : base(message)
        { }
    }
}