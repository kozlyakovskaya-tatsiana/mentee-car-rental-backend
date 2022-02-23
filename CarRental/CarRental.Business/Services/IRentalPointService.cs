using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Business.Models.RentalPoint;

namespace CarRental.Business.Services
{
    public interface IRentalPointService
    {
        public Task<RentalPointModel> CreateRentalPoint(RentalPointModel model);
        public Task<IEnumerable<RentalPointWithCoordsModel>> GetAllRentalPoints();
        public Task<IEnumerable<RentalPointWithCoordsModel>> GetRentalPointsByCity(Guid cityId);
        public Task<RentalPointModel> RemoveRentalPoint(Guid id);
        public Task<RentalPointModel> GetRentalPointById(Guid id);
    }
}