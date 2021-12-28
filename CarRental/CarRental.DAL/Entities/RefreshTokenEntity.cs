using System;

namespace CarRental.DAL.Entities
{
    public class RefreshTokenEntity : BaseEntity
    {
        public string Token { get; set; }
        public DateTime Expired { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
