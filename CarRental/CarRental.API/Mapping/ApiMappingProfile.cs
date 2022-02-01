﻿using AutoMapper;
using CarRental.API.Models.Requests;
using CarRental.Business.Models;
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
                .ForMember(u => u.Username,
                    opt => opt
                        .MapFrom(ur => ur.Email));
            CreateMap<LoginRequest, LoginModel>();
            CreateMap<GetTokenPairRequest, TokenPairModel>();
            CreateMap<RoleCreateRequest, RoleCreateModel>();
            CreateMap<AddRoleRequest, UserRoleModel>();
            CreateMap<ValidateAccessTokenRequest, TokenValidationModel>();
            CreateMap<AddCountryRequest, CountryModel>();
            CreateMap<AddCityRequest, CityModel>();
            CreateMap<AddNewLocationRequest, LocationModel>();
            CreateMap<AddNewRentalPointRequest, RentalPointModel>();
        }
    }
}