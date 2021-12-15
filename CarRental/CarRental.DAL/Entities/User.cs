using System.Collections.Generic;

namespace CarRental.DAL.Entities
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<BookingReport> Reports { get; set; }
    }
}