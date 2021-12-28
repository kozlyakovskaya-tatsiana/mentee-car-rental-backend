using AutoMapper;
using CarRental.API.Models.Requests;
using CarRental.API.Models.Responses;
using CarRental.Business.Models.Role;
using CarRental.Business.Models.Token;
using CarRental.Business.Models.User;

namespace CarRental.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequest, RegisterModel>()
                .ForMember(u => u.Username,
                    opt => opt
                        .MapFrom(ur => ur.Email));
            CreateMap<LoginRequest, LoginModel>();
            CreateMap<TokenPairModel, TokenPairResponse>();
            CreateMap<TokenPairRequest, TokenRevokeModel>();
            CreateMap<TokenPairRequest, TokenPairModel>();
            CreateMap<RoleCreateRequest, RoleCreateModel>();
            CreateMap<AddRoleRequest, UserRoleModel>();
        }
    }
}
