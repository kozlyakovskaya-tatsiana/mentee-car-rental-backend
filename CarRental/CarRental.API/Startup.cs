using CarRental.API.Extensions;
using CarRental.API.Mapping;
using CarRental.Business.Mapping;
using CarRental.Common.Options;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace CarRental.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.LoadConfigurations(Configuration);

            services.AddDbCollection(Configuration.GetSection(ConnectionOptions.SectionName).Get<ConnectionOptions>());

            services.AddControllers()
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<Startup>();
                })
                .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddRepositories();

            services.AddCors();

            services.AddScoped<DataInitializer>();

            services.AddCarRentalServices();

            services.AddSwaggerEnvironment();

            services.AddAutoMapper(typeof(ApiMappingProfile), typeof(BllMappingProfile));

            services.AddUserAuthentication(Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>());
        }

        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env,
            DataInitializer seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API"); });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            );

            app.ConfigureCustomExceptionMiddleware();

            seeder.SeedData().GetAwaiter().GetResult();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            

        }
    }
}