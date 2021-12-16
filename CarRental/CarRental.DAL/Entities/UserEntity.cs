using System.Collections.Generic;

namespace CarRental.DAL.Entities
{
    public class UserEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<BookingReportEntity> Reports { get; set; }
    }
}