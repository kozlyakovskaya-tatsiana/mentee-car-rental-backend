using CarRental.API.Extensions;
using CarRental.Business.Options;
using CarRental.Business.Services;
using CarRental.Business.Services.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CarRental.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JwtOptions>(Configuration.GetSection(JwtOptions.SectionName));
            services.Configure<ConnectionOptions>(Configuration.GetSection(ConnectionOptions.SectionName));

            services.AddDbCollection(Configuration.GetSection(ConnectionOptions.SectionName).Get<ConnectionOptions>());

            services.AddControllers();

            services.AddRepositories();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddSwaggerEnvironment();

            services.AddAutoMapper(typeof(CarRental.API.Mapping.MappingProfile));
            services.AddAutoMapper(typeof(CarRental.Business.Mapping.MappingProfile));

            services.AddUserAuthentication(Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>());
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API"); });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}