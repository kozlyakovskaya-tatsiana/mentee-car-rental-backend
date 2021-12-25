using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Business.Models;
using CarRental.Business.Models.Token;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;

namespace CarRental.Business.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IMapper _mapper;
        public AuthService(
            IRefreshTokenRepository refreshTokenRepository,
            IMapper mapper
            )
        {
            _refreshTokenRepository = refreshTokenRepository;
            _mapper = mapper;
        }




    }
}
