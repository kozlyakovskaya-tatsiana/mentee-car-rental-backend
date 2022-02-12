using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Business.Identity.Role;
using CarRental.Common.Options;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CarRental.API.Extensions
{
    public class DataInitializer
    {
        private readonly CarRentalDbContext _context;

        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<RoleEntity> _roleManager;

        private readonly DefaultUserOptions _defaultUserOptions;

        public DataInitializer(
            CarRentalDbContext context, 
            UserManager<UserEntity> userManager, 
            RoleManager<RoleEntity> roleManager,
            IOptions<DefaultUserOptions> defaultUserOptions
            )
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _defaultUserOptions = defaultUserOptions.Value;
        }

        // public async Task<IApplicationBuilder> DataInitializer(
        //     UserManager<UserEntity> userManager
        //     )
        // { 
        //     await context.Database.MigrateAsync();
        //
        //     if (context.Roles.IsNullOrEmpty())
        //     {
        //         await SeedRoles(context);
        //     }
        //
        //     if (context.Users.IsNullOrEmpty())
        //     {
        //         await SeedUsers(context, userManager);
        //     }
        //
        //     await context.SaveChangesAsync();
        // }

        private static async Task SeedRoles(CarRentalDbContext context)
        {
            var adminRole = new RoleEntity
            {
                Id = Guid.NewGuid(),
                Name = Role.AdminRole,
                NormalizedName = Role.AdminRole.ToUpper()
            };

            var managerRole = new RoleEntity
            {
                Id = Guid.NewGuid(),
                Name = Role.ManagerRole,
                NormalizedName = Role.ManagerRole.ToUpper()
            };

            var userRole = new RoleEntity
            {
                Id = Guid.NewGuid(),
                Name = Role.UserRole,
                NormalizedName = Role.UserRole.ToUpper()
            };

            var roles = new List<RoleEntity>
            {
                adminRole, managerRole, userRole
            };

            foreach (var role in roles)
            {
                context.Roles.Add(role);
            }


        }

        private static async Task SeedUsers(
            CarRentalDbContext context,
            UserManager<UserEntity> userManager
            )
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
            var options = new DefaultUserOptions();
            config.GetSection(DefaultUserOptions.SectionName).Bind(options);

            var hasher = new PasswordHasher<UserEntity>();

            var admin = new UserEntity
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

            await userManager.AddToRoleAsync(admin, Role.AdminRole);

            var manager = new UserEntity
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

            await userManager.AddToRoleAsync(manager, Role.ManagerRole);

            var user = new UserEntity
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

            await userManager.AddToRoleAsync(user, Role.UserRole);

            context.Users.Add(admin);
            context.Users.Add(manager);
            context.Users.Add(user);
        }
    }
}