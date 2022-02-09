﻿using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories
{
    public interface ICarBrandRepository : IBaseRepository<CarBrandEntity>
    {
        public CarBrandEntity GetByName(string name);
    }
}