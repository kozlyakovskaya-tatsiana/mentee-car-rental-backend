using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;

namespace CarRental.DAL.Repositories
{
    public class CountryRepository : BaseRepository<CountryEntity>, ICountryRepository
    {
        public CountryRepository(CarRentalDbContext context) : base(context)
        { }
    }
}