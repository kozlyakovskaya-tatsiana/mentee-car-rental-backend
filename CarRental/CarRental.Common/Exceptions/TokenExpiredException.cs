using System;

namespace CarRental.Common.Exceptions
{
    public class TokenExpiredException : Exception
    {
        public TokenExpiredException() { }

        public TokenExpiredException(string message) : base(message) { }
    }
}
