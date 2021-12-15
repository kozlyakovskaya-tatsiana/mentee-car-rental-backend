using System.Collections.Generic;

namespace CarRental.DAL.Entities
{
    public class User
    {
        public string Name { get; set; }
        public string SurName { get; set; }

        public List<BookingReport> Reports { get; set; }
    }
}
