using CarRental.Common.Enums;

namespace CarRental.Business.Models.Car
{
    public class CarInfoModel
    {
        public string Model { get; set; }
        public FuelType Fuel { get; set; }
        public double FuelConsumption { get; set; }
        public TransmissionType Transmission { get; set; }
        public int QuantityOfSeats { get; set; }
        public double PricePerHour { get; set; }
        public CarStatus Status { get; set; }
    }
}
