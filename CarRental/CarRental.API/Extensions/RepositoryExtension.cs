using CarRental.DAL;
using CarRental.DAL.Repositories;
using CarRental.DAL.Repositories.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.API.Extensions
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepositories(
            this IServiceCollection services
        )
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            services.AddScoped<IBookingReportRepository, BookingReportRepository>();
            services.AddScoped<ICarBrandRepository, CarBrandRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IRentalPointRepository, RentalPointRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
