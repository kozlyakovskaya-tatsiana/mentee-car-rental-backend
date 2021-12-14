using System;
using System.Collections.Generic;
using System.Text;

namespace carRental.DAL.Entities
{
    class Car
    {
        public string Id { get; set; }
        long CarTypeId { get; set; }
        long UserId { get; set; }
        long PlaceId { get; set; }
        float PricePerHour { get; set; }
        Content Photo { get; set; }
        string Coordinates { get; set; }
        DateTime BusyTime { get; set; }
        bool IsBlocked { get; set; }
    }
}
