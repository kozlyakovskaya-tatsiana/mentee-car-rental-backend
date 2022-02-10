using System;
using System.Collections.Generic;
using System.Linq;
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
        private static Guid AdminRoleId { get; set; }
        private static Guid AdminId { get; set; }
        private static Guid ManagerRoleId { get; set; }
        private static Guid ManagerId { get; set; }
        private static Guid UserRoleId { get; set; }
        private static Guid UserId { get; set; }

        public static IHost DataInitialize(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<CarRentalDbContext>();

            context.Database.Migrate();

            if (context.Roles.IsNullOrEmpty())
            {
                SeedRoles(context);
            }

            if (context.Users.IsNullOrEmpty())
            {
                SeedUsers(context);
            }

            context.SaveChanges();

            if (context.UserRoles.IsNullOrEmpty())
            {
                SeedUserRoles(context);
            }

            context.SaveChanges();

            return host;
        }

        private static void SeedRoles(CarRentalDbContext context)
        {
            var adminRole = new RoleEntity
            {
                Id = Guid.NewGuid(),
                Name = Role.AdminRole,
                NormalizedName = Role.AdminRole.ToUpper()
            };
            AdminRoleId = adminRole.Id;

            var managerRole = new RoleEntity
            {
                Id = Guid.NewGuid(),
                Name = Role.ManagerRole,
                NormalizedName = Role.ManagerRole.ToUpper()
            };
            ManagerRoleId = managerRole.Id;

            var userRole = new RoleEntity
            {
                Id = Guid.NewGuid(),
                Name = Role.UserRole,
                NormalizedName = Role.UserRole.ToUpper()
            };
            UserRoleId = userRole.Id;

            var roles = new List<RoleEntity>
            {
                adminRole, managerRole, userRole
            };

            foreach (var role in roles)
            {
                context.Roles.Add(role);
            }
        }

        private static void SeedUsers(CarRentalDbContext context)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
            var options = new DefaultUserOptions();
            config.GetSection(DefaultUserOptions.SectionName).Bind(options);

            var hasher = new PasswordHasher<UserEntity>();

            var admin = new UserEntity()
            {
                Id = Guid.NewGuid(),
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
            AdminId = admin.Id;

            var manager = new UserEntity()
            {
                Id = Guid.NewGuid(),
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
            ManagerId = manager.Id;

            var user = new UserEntity()
            {
                Id = Guid.NewGuid(),
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
            UserId = user.Id;

            context.Users.Add(admin);
            context.Users.Add(manager);
            context.Users.Add(user);
        }

        private static void SeedUserRoles(CarRentalDbContext context)
        {
            var adminRoleUser = new IdentityUserRole<Guid>()
            {
                RoleId = AdminRoleId,
                UserId = AdminId
            };
            var managerRoleUser = new IdentityUserRole<Guid>()
            {
                RoleId = ManagerRoleId,
                UserId = ManagerId
            };
            var userRoleUser = new IdentityUserRole<Guid>()
            {
                RoleId = UserRoleId,
                UserId = UserId
            };

            var seedRoles = new List<IdentityUserRole<Guid>>
            {
                adminRoleUser, managerRoleUser, userRoleUser
            };

            foreach (var role in seedRoles)
            {
                context.UserRoles.Add(role);
            }
        }
    }
}