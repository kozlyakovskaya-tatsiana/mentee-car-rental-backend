using System;

namespace CarRental.Common.Exceptions
{
    public class NotVerifiedException : Exception
    {
        public NotVerifiedException() { }

        public NotVerifiedException(string message) : base(message) { }
    }
}

