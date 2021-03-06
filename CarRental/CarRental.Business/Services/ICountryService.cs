using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Business.Models.Location;

namespace CarRental.Business.Services
{
    public interface ICountryService
    {
        public Task<CountryModel> CreateNewCountry(CountryModel model);
        public Task<IEnumerable<CountryModel>> GetAllCountries();
    }
}
