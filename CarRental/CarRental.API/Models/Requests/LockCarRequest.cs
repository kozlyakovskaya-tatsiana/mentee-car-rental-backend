using System;

namespace CarRental.API.Models.Requests
{
    public class LockCarRequest
    {
        public Guid CarId { get; set; }
    }
}