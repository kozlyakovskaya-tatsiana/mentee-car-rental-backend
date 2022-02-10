﻿using AutoMapper;
using CarRental.API.Models.Requests;
using CarRental.Business.Models;
using CarRental.Business.Models.Car;
using CarRental.Business.Models.Location;
using CarRental.Business.Models.Role;
using CarRental.Business.Models.Token;
using CarRental.Business.Models.User;

namespace CarRental.API.Mapping
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<RegisterRequest, RegisterModel>()
                .ForMember(model => model.Username,
                    opt => opt
                        .MapFrom(source => source.Email));

            CreateMap<LoginRequest, LoginModel>();

            CreateMap<GetTokenPairRequest, TokenPairModel>();

            CreateMap<CreateRoleRequest, CreateRoleModel>();
            CreateMap<AddRoleRequest, UserRoleModel>();

            CreateMap<ValidateAccessTokenRequest, TokenValidationModel>();

            CreateMap<AddCountryRequest, CountryModel>();
            CreateMap<AddCityRequest, CityModel>();
            CreateMap<CreateLocationRequest, LocationModel>();

            CreateMap<CreateRentalPointRequest, RentalPointModel>();
            CreateMap<CreateCarBrandRequest, CarBrandModel>();
            CreateMap<CreateCarRequest, CreatingCarModel>()
                .ForPath(model => model.Brand.Name, 
                    opt => opt
                        .MapFrom(source => source.Brand));
        }
    }
}