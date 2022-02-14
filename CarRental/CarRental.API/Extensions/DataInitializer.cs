using System;
using System.Threading.Tasks;
using CarRental.Business.Identity.Role;
using CarRental.Common.Options;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CarRental.API.Extensions
{
    public class DataInitializer
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<RoleEntity> _roleManager;

        private readonly DefaultUserOptions _defaultUserOptions;
        private readonly CarRentalDbContext _context;

        public DataInitializer(
            UserManager<UserEntity> userManager,
            RoleManager<RoleEntity> roleManager,
            IOptions<DefaultUserOptions> defaultUserOptions,
            CarRentalDbContext context
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _defaultUserOptions = defaultUserOptions.Value;
            _context = context;
        }

        public async Task SeedData()
        {
            await _context.Database.MigrateAsync();
            if(_context.Roles.IsNullOrEmpty())
            {
                await SeedRoles();
            }
            if(_context.Users.IsNullOrEmpty())
            {
                await SeedUsers();
            }
        }

        private async Task SeedRoles()
        {
            var adminRole = new RoleEntity
            {
                Name = Role.AdminRole,
                NormalizedName = Role.AdminRole.ToUpper()
            };
            await _roleManager.CreateAsync(adminRole);

            var managerRole = new RoleEntity
            {
                Name = Role.ManagerRole,
                NormalizedName = Role.ManagerRole.ToUpper()
            };
            await _roleManager.CreateAsync(managerRole);

            var userRole = new RoleEntity
            {
                Name = Role.UserRole,
                NormalizedName = Role.UserRole.ToUpper()
            };
            await _roleManager.CreateAsync(userRole);
        }

        private async Task SeedUsers()
        {
            var hasher = new PasswordHasher<UserEntity>();

            var admin = new UserEntity
            {
                FirstName = _defaultUserOptions.AdminFirstName,
                LastName = _defaultUserOptions.AdminLastName,
                Email = _defaultUserOptions.AdminEmail,
                NormalizedEmail = _defaultUserOptions.AdminEmail.ToUpper(),
                UserName = _defaultUserOptions.AdminEmail,
                NormalizedUserName = _defaultUserOptions.AdminEmail.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString()
            };
            admin.PasswordHash = hasher.HashPassword(admin, _defaultUserOptions.AdminPassword);

            await _userManager.CreateAsync(admin);
            await _userManager.AddToRoleAsync(admin, Role.AdminRole);

            var manager = new UserEntity
            {
                FirstName = _defaultUserOptions.ManagerFirstName,
                LastName = _defaultUserOptions.ManagerLastName,
                Email = _defaultUserOptions.ManagerEmail,
                NormalizedEmail = _defaultUserOptions.ManagerEmail.ToUpper(),
                UserName = _defaultUserOptions.ManagerEmail,
                NormalizedUserName = _defaultUserOptions.ManagerEmail.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString()
            };
            manager.PasswordHash = hasher.HashPassword(manager, _defaultUserOptions.ManagerPassword);

            await _userManager.CreateAsync(manager);
            await _userManager.AddToRoleAsync(manager, Role.ManagerRole);

            var user = new UserEntity
            {
                FirstName = _defaultUserOptions.UserFirstName,
                LastName = _defaultUserOptions.UserLastName,
                Email = _defaultUserOptions.UserEmail,
                NormalizedEmail = _defaultUserOptions.UserEmail.ToUpper(),
                UserName = _defaultUserOptions.UserEmail,
                NormalizedUserName = _defaultUserOptions.UserEmail.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString()
            };
            user.PasswordHash = hasher.HashPassword(user, _defaultUserOptions.UserPassword);

            await _userManager.CreateAsync(user);
            await _userManager.AddToRoleAsync(user, Role.UserRole);
        }
    }
}