using System;

namespace CarRental.Common.Exceptions
{
    public class BadAuthorizeException : Exception
    {
        public BadAuthorizeException() { }

        public BadAuthorizeException(string message) : base(message) { }
    }
}
