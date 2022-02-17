using CarRental.Business.Services;
using CarRental.Business.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.API.Extensions
{
    public static class CarRentalServicesExtension
    {
        public static IServiceCollection AddCarRentalServices(
            this IServiceCollection services
        )
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IRentalPointService, RentalPointService>();
            services.AddScoped<ICarBrandService, CarBrandService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICountryService, CountryService>();

            return services;
        }
    }
}
