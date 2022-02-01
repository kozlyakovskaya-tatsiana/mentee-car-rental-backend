using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories
{
    public interface ICityRepository : IBaseRepository<CityEntity>
    {
        public CityEntity GetCityByName(string name);
    }
}