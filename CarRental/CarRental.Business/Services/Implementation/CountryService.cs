using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Business.Models.Location;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;

namespace CarRental.Business.Services.Implementation
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        private readonly IMapper _mapper;

        public CountryService(
            ICountryRepository countryRepository,
            IMapper mapper
        )
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<CountryModel> AddNewCountry(CountryModel model)
        {
            var countryEntity = _mapper.Map<CountryModel, CountryEntity>(model);
            var country = await _countryRepository.Add(countryEntity);
            var result = _mapper.Map<CountryEntity, CountryModel>(country);

            return result;
        }

        public async Task<IEnumerable<CountryModel>> GetAllCountries()
        {
            var countries = _countryRepository.GetAll();

            return countries.Select(country => _mapper.Map<CountryEntity, CountryModel>(country)).ToArray();
        }
    }
}