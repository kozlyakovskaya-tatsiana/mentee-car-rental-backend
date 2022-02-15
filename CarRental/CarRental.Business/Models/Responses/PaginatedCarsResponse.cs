using System.Collections.Generic;
using CarRental.Business.Models.Car;

namespace CarRental.Business.Models.Responses
{
    public class PaginatedCarsResponse
    {
        public IEnumerable<CarExtendedInfoModel> Cars { get; set; }
        public int QuantityOfResults { get; set; }
    }
}
