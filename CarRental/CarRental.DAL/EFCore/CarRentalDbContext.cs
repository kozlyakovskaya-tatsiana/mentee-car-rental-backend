using System;
using CarRental.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.EFCore
{
    public sealed class CarRentalDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
    {

        public CarRentalDbContext(DbContextOptions options) : base(options)
        { }


        public DbSet<CarBrandEntity> CarBrands { get; set; }
        public DbSet<CarEntity> Cars { get; set; }
        public DbSet<BookingReportEntity> Reports { get; set; }
        public DbSet<CountryEntity> Countries { get; set; }
        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<LocationEntity> Locations { get; set; }
        public DbSet<AttachmentEntity> Attachments { get; set; }
        public DbSet<RentalPointEntity> RentalPoints { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarRentalDbContext).Assembly);
        }
    }
}