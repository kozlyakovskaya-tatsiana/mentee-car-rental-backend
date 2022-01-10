using System.Threading.Tasks;
using CarRental.Business.Models.Location;

namespace CarRental.Business.Services
{
    public interface ILocationService
    {
        public Task<CountryModel> AddNewCountry(CountryModel model);
    }
}
