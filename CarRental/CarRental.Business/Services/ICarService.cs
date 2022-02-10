using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Business.Models.Car;

namespace CarRental.Business.Services
{
    public interface ICarService
    {
        public Task<IEnumerable<CarInfoModel>> GetAllCars();
        public Task<CarInfoModel> GetCarInfo(Guid id);
        public Task<CarInfoModel> RemoveCar(Guid id);
        public Task<CarInfoModel> ModifyCar(Guid id, CarInfoModel model);
        public Task<CarInfoModel> CreateCar(CreatingCarModel model);

        public Task<IEnumerable<CarBrandModel>> GetCarBrands();
        public Task<CarBrandModel> CreateBrand(CarBrandModel model);
    }
}