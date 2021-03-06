using System;
using CarRental.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CarRental.DAL.EFCore
{
    public sealed class CarRentalDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
    {
        public DbSet<CarBrandEntity> CarBrands { get; set; }
        public DbSet<CarEntity> Cars { get; set; }
        public DbSet<BookingReportEntity> Reports { get; set; }
        public DbSet<CountryEntity> Countries { get; set; }
        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<LocationEntity> Locations { get; set; }
        public DbSet<AttachmentEntity> Attachments { get; set; }
        public DbSet<RentalPointEntity> RentalPoints { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        public CarRentalDbContext(DbContextOptions options) : base(options)
        { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Log.Information("Init database...");
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarRentalDbContext).Assembly);
            Log.Information("Complete.");
        }
    }
}