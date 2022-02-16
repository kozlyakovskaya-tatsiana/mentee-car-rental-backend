using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Business.Models.Car;

namespace CarRental.Business.Services
{
    public interface ICarBrandService
    {
        public Task<IEnumerable<CarBrandModel>> GetCarBrands();
        public Task<CarBrandModel> CreateBrand(CarBrandModel model);
    }
}
