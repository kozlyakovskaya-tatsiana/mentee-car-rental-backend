using System;
using CarRental.DAL.Entities;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CarRental.DAL.EFCore
{
    public sealed class CarRentalDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
    {
        private readonly ConnectionOptions _connection;
        public CarRentalDbContext(IOptions<ConnectionOptions> connectionOptions) : base()
        {
            _connection = connectionOptions.Value;
        }

        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options,
            IOptions<ConnectionOptions> connectionOptions)
        {
            _connection = connectionOptions.Value;
        }


        public DbSet<CarBrandEntity> CarBrands { get; set; }
        public DbSet<CarEntity> Cars { get; set; }
        public DbSet<BookingReportEntity> Reports { get; set; }
        public DbSet<CountryEntity> Countries { get; set; }
        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<LocationEntity> Locations { get; set; }
        public DbSet<AttachmentEntity> Attachments { get; set; }
        public DbSet<RentalPointEntity> RentalPoints { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TestDB;Username=postgres;Password=root;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarRentalDbContext).Assembly);
        }
    }
}