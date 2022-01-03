using AutoMapper;
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
        }
    }
}