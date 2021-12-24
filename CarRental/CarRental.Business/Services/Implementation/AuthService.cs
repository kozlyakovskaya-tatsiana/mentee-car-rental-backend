using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Business.Models;
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

        public bool IsRefreshExpired(TokenPairModel pair)
        {
            var tokens =  _refreshTokenRepository.Get(pair.RefreshToken.Token);
            return tokens.Result.Expired > DateTime.Now;
        }

        public async Task<TokenPairModel> UpdateJwToken(UserEntity user, JwtSecurityToken newJwt)
        {
            var data = await _refreshTokenRepository.Get(user.Id);
            await _refreshTokenRepository.Update(data);
            var result = _mapper.Map<RefreshTokenEntity, TokenPairModel>(data);
            return result;
        }

    }
}
