using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Business.Models.Location;

namespace CarRental.Business.Services
{
    public interface ILocationService
    {
        public Task<CountryModel> AddNewCountry(CountryModel model);
        public Task<IEnumerable<CountryModel>> GetAllCountries();

        public Task<CityModel> AddNewCity(CityModel model);
        public Task<IEnumerable<CityModel>> GetAllCities();
        public Task<IEnumerable<CityModel>> GetCitiesByCountryId(Guid countryId);

        public Task<LocationModel> AddNewLocation(LocationModel model);
    }
}