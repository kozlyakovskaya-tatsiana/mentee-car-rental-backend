using System;
using System.Collections.Generic;
using System.Text;

namespace carRental.DAL.Entities
{
    class Car
    {
        public string Id { get; set; }
        long carTypeId { get; set; }
        long userId { get; set; }
        long placeId { get; set; }
        float pricePerHour { get; set; }
        Content photo { get; set; }
        string coordinates { get; set; }
        DateTime busyTime { get; set; }
        bool isBlocked { get; set; }
    }
}
