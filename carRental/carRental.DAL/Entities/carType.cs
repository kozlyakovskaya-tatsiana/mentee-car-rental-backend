using System;
using System.Collections.Generic;
using System.Text;

namespace carRental.DAL.Entities
{
    // TODO move ENUM to Common?
    enum TransmisionType
    {
        auto,
        mechanic
    }
    class CarType
    {
        public string Id { get; set; }
        Brand BrandName { get; set; }
        string Model { get; set; }
        float FuelConsumption { get; set; }
        TransmisionType Transmision { get; set; }
        int QuatityOfSeats { get; set; }
    }
}
