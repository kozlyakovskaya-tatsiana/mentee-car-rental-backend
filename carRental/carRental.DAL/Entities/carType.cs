using System;
using System.Collections.Generic;
using System.Text;

namespace carRental.DAL.Entities
{
    // TODO move ENUM to different file
    enum TransmisionType
    {
        auto,
        mechanic
    }
    class carType
    {
        public string Id { get; set; }
        Brand brand { get; set; }
        string model { get; set; }
        float fuelConsumption { get; set; }
        TransmisionType transmision { get; set; }
        int quatityOfSeats { get; set; }
    }
}
