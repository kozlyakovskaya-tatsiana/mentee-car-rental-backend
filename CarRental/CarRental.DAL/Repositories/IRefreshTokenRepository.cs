using System;
using System.Threading.Tasks;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories
{
    public interface IRefreshTokenRepository :IBaseRepository<RefreshTokenEntity>
    {
        Task<RefreshTokenEntity> Get(Guid userId, string token);
        Task Revoke(Guid id);
    }
}