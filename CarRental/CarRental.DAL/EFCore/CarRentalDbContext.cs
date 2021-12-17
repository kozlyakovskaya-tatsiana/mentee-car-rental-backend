using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.EFCore
{
    public sealed class CarRentalDbContext : DbContext
    {
        public CarRentalDbContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<CarBrandEntity> CarBrands { get; set; }
        public DbSet<CarEntity> Cars { get; set; }
        public DbSet<BookingReportEntity> Reports { get; set; }
        public DbSet<CountryEntity> Countries { get; set; }
        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<LocationEntity> Locations { get; set; }
        public DbSet<AttachmentEntity> Attachments { get; set; }
        public DbSet<RentalPointEntity> RentalPoints { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;" +
                                     "Port=5432;Database=postgres;" +
                                     "Username=postgres;" +
                                     "Password=root;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarRentalDbContext).Assembly);
        }
    }
}