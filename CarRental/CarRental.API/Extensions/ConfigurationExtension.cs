using CarRental.Common.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.API.Extensions
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection LoadConfigurations(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
            services.Configure<ConnectionOptions>(configuration.GetSection(ConnectionOptions.SectionName));
            services.Configure<DefaultUserOptions>(configuration.GetSection(DefaultUserOptions.SectionName));
            services.Configure<PaginationOptions>(configuration.GetSection(PaginationOptions.SectionName));

            return services;
        }
    }
}
