using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using CarRental.Business.Models;
using CarRental.Business.Models.Attachment;
using CarRental.Business.Models.BookingReport;
using CarRental.Business.Models.Car;
using CarRental.Business.Models.Location;
using CarRental.Business.Models.RentalPoint;
using CarRental.Business.Models.Role;
using CarRental.Business.Models.Token;
using CarRental.Business.Models.User;
using CarRental.Common.Enums;
using CarRental.DAL.Entities;

namespace CarRental.Business.Mapping
{
    public class BllMappingProfile : Profile
    {
        public override string ProfileName => "BLMappings";

        public BllMappingProfile()
        {
            CreateMap<RegisterModel, UserEntity>()
                .ForMember(entity => entity.UserName,
                    opt => opt
                        .MapFrom(source => source.Email));

            CreateMap<RefreshTokenEntity, TokenPairModel>()
                .ForMember(model => model.RefreshToken,
                    opt => opt
                        .MapFrom(source => source.Token));

            CreateMap<LoginModel, UserEntity>();

            CreateMap<CreateRoleModel, RoleEntity>();

            CreateMap<UserEntity, UserInfoModel>();
            CreateMap<CreateCarModel, CarEntity>()
                .ForMember(entity => entity.Photos, opt => opt
                    .MapFrom(source => source.Photos));
            CreateMap<CountryModel, CountryEntity>();
            CreateMap<CountryEntity, CountryModel>();

            CreateMap<CityModel, CityEntity>()
                .ForMember(entity => entity.Locations, opt => opt
                    .MapFrom(source => source.Locations));
            CreateMap<CityEntity, CityModel>();

            CreateMap<LocationModel, LocationEntity>()
                .ForPath(entity => entity.City, opt => opt.Ignore());
            CreateMap<LocationEntity, LocationModel>()
                .ForMember(model => model.City, opt => opt
                    .MapFrom(source => source.City.Name));

            CreateMap<RentalPointModel, RentalPointEntity>()
                .ForPath(entity => entity.Location.City.Name, opt => opt
                    .MapFrom(source => source.Location.City))
                .ForPath(entity => entity.Location.City.Country.Name,
                    opt => opt
                        .MapFrom(source => source.Location.Country))
                .ForPath(entity => entity.Location.Latitude, opt => opt
                    .MapFrom(source => source.Location.Latitude))
                .ForPath(entity => entity.Location.Longitude, opt => opt
                    .MapFrom(source => source.Location.Longitude))
                .ForPath(entity => entity.Location.Address, opt => opt
                    .MapFrom(source => source.Location.Address));
            CreateMap<RentalPointEntity, RentalPointModel>()
                .ForMember(model => model.Location, opt => opt
                    .MapFrom(source => source.Location));

            CreateMap<RentalPointEntity, RentalPointWithCoordsModel>()
                .ForMember(model => model.Location, opt => opt
                    .MapFrom(source => source.Location));

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
                    .MapFrom(source => source.Photos))
                .ForMember(model => model.RentalPointId, opt => opt
                    .MapFrom(source => source.RentalPointId));

            CreateMap<BookingReportEntity, BookingReportInfoModel>()
                .ForMember(model => model.UserId, opt => opt
                    .MapFrom(source => source.UserId))
                .ForMember(model => model.CarId, opt => opt
                    .MapFrom(source => source.CarId))
                .ForMember(model => model.Status, opt => opt
                    .MapFrom(source => Enum.GetName(typeof(BookingStatus), source.Status)));
        }
    }
}