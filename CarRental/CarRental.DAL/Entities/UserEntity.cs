using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CarRental.DAL.Entities
{
    public class UserEntity : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<BookingReportEntity> Reports { get; set; }

        public List<RefreshTokenEntity> RefreshTokens { get; set; }
    }
}