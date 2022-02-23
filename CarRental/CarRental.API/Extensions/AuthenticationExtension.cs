using System;
using System.Text;
using CarRental.Business.Identity.Policy;
using CarRental.Business.Identity.Role;
using CarRental.Common.Options;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CarRental.API.Extensions
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddUserAuthentication(
            this IServiceCollection services,
            JwtOptions jwt
        )
        {
            services
                .AddAuthorization(options =>
                {
                    options.AddPolicy(Policy.AdminPolicy, policy =>
                        policy.RequireRole(Role.AdminRole));
                })
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = jwt.ValidateIssuer,
                        ValidateAudience = jwt.ValidateAudience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,

                        ValidIssuer = jwt.Issuer,
                        ValidAudience = jwt.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
                    };
                });

            services.AddIdentityCore<UserEntity>()
                .AddRoles<RoleEntity>()
                .AddEntityFrameworkStores<CarRentalDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}