using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Business.Models.Location;

namespace CarRental.Business.Services
{
    public interface ICityService
    {
        public Task<CityModel> CreateNewCity(CityModel model);
        public Task<IEnumerable<CityModel>> GetAllCities();
        public Task<IEnumerable<CityModel>> GetCitiesByCountryId(Guid countryId);
    }
}
