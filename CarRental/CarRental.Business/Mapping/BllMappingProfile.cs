using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using CarRental.Business.Models;
using CarRental.Business.Models.Car;
using CarRental.Business.Models.Location;
using CarRental.Business.Models.Role;
using CarRental.Business.Models.Token;
using CarRental.Business.Models.User;
using CarRental.DAL.Entities;

namespace CarRental.Business.Mapping
{
    public class BllMappingProfile : Profile
    {
        public override string ProfileName => "BLMappings";

        public BllMappingProfile()
        {
            CreateMap<RegisterModel, UserEntity>().ForMember(u => u.UserName,
                opt => opt.MapFrom(ur => ur.Email));
            CreateMap<RefreshTokenEntity, TokenPairModel>()
                .ForMember(x => x.RefreshToken,
                    xr => xr.MapFrom(az => az.Token));
            CreateMap<LoginModel, UserEntity>();
            CreateMap<RoleCreateModel, RoleEntity>();
            CreateMap<UserEntity, UserInfoModel>();
            CreateMap<CreatingCarModel, CarEntity>()
                .ForMember(entity => entity.Photos, opt => opt
                    .MapFrom(source => source.Photos));
            CreateMap<CountryModel, CountryEntity>();
            CreateMap<CountryEntity, CountryModel>();

            CreateMap<CityModel, CityEntity>()
                .ForMember(l => l.Locations, opt => opt
                    .MapFrom(ls => ls.Locations));
            CreateMap<CityEntity, CityModel>();

            CreateMap<LocationModel, LocationEntity>()
                .ForPath(s => s.City, dest => dest.Ignore());
            CreateMap<LocationEntity, LocationModel>();

            CreateMap<RentalPointModel, RentalPointEntity>()
                .ForPath(entity => entity.Location.City.Name, opt => opt.MapFrom(source => source.Location.City))
                .ForPath(entity => entity.Location.City.Country.Name,
                    opt => opt.MapFrom(source => source.Location.Country))
                .ForPath(entity => entity.Location.Latitude, opt => opt.MapFrom(source => source.Location.Latitude))
                .ForPath(entity => entity.Location.Longitude, opt => opt.MapFrom(source => source.Location.Longitude))
                .ForPath(entity => entity.Location.Address, opt => opt.MapFrom(source => source.Location.Address));
            CreateMap<RentalPointEntity, RentalPointModel>()
                .ForPath(s => s.Location.City, dest => dest.MapFrom(d => d.Location.City.Name));

            CreateMap<RentalPointEntity, RentalPointWithCoordsModel>()
                .ForMember(dest => dest.Location, opt => opt
                    .MapFrom(res => res.Location));

            CreateMap<CarBrandEntity, CarBrandModel>();
            CreateMap<CarBrandModel, CarBrandEntity>();

            CreateMap<string, byte[]>().ConvertUsing(s => Convert.FromBase64String(s));
            CreateMap<byte[], string>().ConvertUsing(s => Convert.ToBase64String(s));

            CreateMap<AttachmentDTO, AttachmentEntity>()
                .ForMember(entity => entity.Content, opt => opt
                    .MapFrom(source => source.Content));
            CreateMap<AttachmentEntity, AttachmentDTO>()
                .ForMember(model => model.Content, opt => opt
                    .MapFrom(source => source.Content));

            CreateMap<CarEntity, CarInfoModel>();
            CreateMap<CarEntity, CarExtendedInfoModel>()
                .ForPath(model => model.Photos, opt => opt
                    .MapFrom(source => source.Photos));
        }
    }
}