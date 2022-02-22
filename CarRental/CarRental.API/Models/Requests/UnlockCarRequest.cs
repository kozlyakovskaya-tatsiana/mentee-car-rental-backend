using System;

namespace CarRental.API.Models.Requests
{
    public class UnlockCarRequest
    {
        public Guid CarId { get; set; }
    }
}