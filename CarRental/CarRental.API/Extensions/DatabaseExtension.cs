using CarRental.Business.Options;
using CarRental.DAL.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.API.Extensions
{
    public static class DatabaseExtension
    {
        public static IServiceCollection AddDbCollection(
            this IServiceCollection services,
            ConnectionOptions connection
        )
        {
             services.AddDbContext<CarRentalDbContext>(s => { s.UseNpgsql(connection.ConnectionString); });
            return services;
        }
    }
}