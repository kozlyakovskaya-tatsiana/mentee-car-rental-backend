using System;
using System.Collections;
using System.Collections.Generic;
using CarRental.Common.Options;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;
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
            var users = new List<UserEntity>()
            {
                
            };

            foreach (var user in users)
            {
                context.Users.Add(user);
            }


        }
    }
}