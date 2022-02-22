using System;

namespace CarRental.Common.Exceptions
{
    public class TransactionFailedException : Exception
    {
        public TransactionFailedException() { }

        public TransactionFailedException(string message) : base(message) { }
    }
}
