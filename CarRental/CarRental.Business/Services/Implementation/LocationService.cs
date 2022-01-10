using System.Threading.Tasks;
using AutoMapper;
using CarRental.Business.Models.Location;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;

namespace CarRental.Business.Services.Implementation
{
    public class LocationService : ILocationService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ILocationRepository _locationRepository;

        private readonly IMapper _mapper;

        public LocationService(
            ICityRepository cityRepository, 
            ILocationRepository locationRepository, 
            ICountryRepository countryRepository, 
            IMapper mapper
            )
        {
            _cityRepository = cityRepository;
            _locationRepository = locationRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<CountryModel> AddNewCountry(CountryModel model)
        {
            var entity = _mapper.Map<CountryModel, CountryEntity>(model);
            var country = await _countryRepository.Add(entity);
            var result = _mapper.Map<CountryEntity, CountryModel>(country);

            return result;
        }
    }
}
