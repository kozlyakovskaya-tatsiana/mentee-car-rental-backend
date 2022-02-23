using System.Collections.Generic;
using CarRental.Business.Models.Car;

namespace CarRental.Business.Models.Responses
{
    public class PaginatedCarsResult
    {
        public IEnumerable<CarExtendedInfoModel> Cars { get; set; }
        public int TotalCarsCount { get; set; }
    }
}
