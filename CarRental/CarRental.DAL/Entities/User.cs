using System.Collections.Generic;

namespace CarRental.DAL.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<BookingReport> Reports { get; set; }
    }
}