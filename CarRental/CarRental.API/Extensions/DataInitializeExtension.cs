using System;
using System.Collections.Generic;
using CarRental.Business.Identity.Role;
using CarRental.Common.Options;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace CarRental.API.Extensions
{
    public static class DataInitializeExtension
    {
        public static IHost DataInitialize(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<CarRentalDbContext>();

            context.Database.Migrate();

            if (context.Roles.IsNullOrEmpty()) { SeedRoles(context); }
            if (context.Users.IsNullOrEmpty()) { SeedUsers(context); }

            context.SaveChanges();

            return host;
        }

        private static void SeedRoles(CarRentalDbContext context)
        {
            var roles = new List<RoleEntity>()
            {
                new RoleEntity()
                {
                    Name = "superadmin",
                    NormalizedName = "SUPERADMIN"
                },
                new RoleEntity()
                {
                    Name = "manager",
                    NormalizedName = "MANAGER"
                },
                new RoleEntity()
                {
                    Name = "user",
                    NormalizedName = "USER"
                }
            };

            foreach (var role in roles)
            {
                context.Roles.Add(role);
            }

            
        }

        private static void SeedUsers(CarRentalDbContext context)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
            DefaultUserOptions options = new DefaultUserOptions();
            config.GetSection(DefaultUserOptions.SectionName).Bind(options);

            var hasher = new PasswordHasher<UserEntity>();

            var admin = new UserEntity()
            {
                FirstName = options.AdminFirstName,
                LastName = options.AdminLastName,
                Email = options.AdminEmail,
                NormalizedEmail = options.AdminEmail.ToUpper(),
                UserName = options.AdminEmail,
                NormalizedUserName = options.AdminEmail.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var adminPasswordHash = hasher.HashPassword(admin, options.AdminPassword);
            admin.PasswordHash = adminPasswordHash;

            var manager = new UserEntity()
            {
                FirstName = options.ManagerFirstName,
                LastName = options.ManagerLastName,
                Email = options.ManagerEmail,
                NormalizedEmail = options.ManagerEmail.ToUpper(),
                UserName = options.ManagerEmail,
                NormalizedUserName = options.ManagerEmail.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var managerPasswordHash = hasher.HashPassword(manager, options.ManagerPassword);
            manager.PasswordHash = managerPasswordHash;

            var user = new UserEntity()
            {
                FirstName = options.UserFirstName,
                LastName = options.UserLastName,
                Email = options.UserEmail,
                NormalizedEmail = options.UserEmail.ToUpper(),
                UserName = options.UserEmail,
                NormalizedUserName = options.UserEmail.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var userPasswordHash = hasher.HashPassword(user, options.UserPassword);
            user.PasswordHash = userPasswordHash;

            context.Users.Add(admin);
            context.Users.Add(manager);
            context.Users.Add(user);

        }

    }
}