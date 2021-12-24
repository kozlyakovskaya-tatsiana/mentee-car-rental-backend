using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using CarRental.API.Models.Requests;
using CarRental.API.Models.Responses;
using CarRental.Business.Models;
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
            CreateMap<TokenPairModel, TokenPairResponse>()
                .ForMember(j => j.AccessToken,
                    jr => jr
                        .MapFrom(jrs =>
                            new JwtSecurityTokenHandler().WriteToken(jrs.AccessToken)))
                .ForMember(r => r.RefreshToken,
                    sr => sr
                        .MapFrom(srr => srr.RefreshToken.Token));
            CreateMap<TokenPairRequest, TokenPairModel>();
        }
    }
}
