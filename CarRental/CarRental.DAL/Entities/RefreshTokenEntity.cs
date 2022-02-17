using System;

namespace CarRental.DAL.Entities
{
    public class RefreshTokenEntity : BaseEntity
    {
        public string Token { get; set; }
        public DateTimeOffset Expired { get; set; }

        public Guid UserId { get; set; }
        public virtual UserEntity User { get; set; }
    }
}
