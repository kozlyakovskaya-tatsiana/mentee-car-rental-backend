using System.Runtime.InteropServices;
using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Contexts
{
    public sealed class CarRentalDbContext : DbContext
    {
        public DbSet<CarBrand> CarBrands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<BookingReport> Reports { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<RentalPoint> RentalPoints { get; set; }

        public CarRentalDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;" +
                                     "Port=5433;Database=postgres;" +
                                     "Username=postgres;" +
                                     "Password=root");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany<BookingReport>(reports => reports.Reports)
                .WithOne(user => user.User)
                .HasForeignKey(userId => userId.UserId);

            modelBuilder.Entity<Car>()
                .HasMany<BookingReport>(bookingReport => bookingReport.Reports)
                .WithOne(car => car.Car)
                .HasForeignKey(carId => carId.CarId);

            modelBuilder.Entity<Car>()
                .HasMany<Attachment>(attachments => attachments.Photos)
                .WithOne(car => car.Car)
                .HasForeignKey(carId => carId.CarId);

            modelBuilder.Entity<CarBrand>()
                .HasMany<Car>(cars => cars.Cars)
                .WithOne(carBrand => carBrand.Brand)
                .HasForeignKey(brandId => brandId.BrandId);

            modelBuilder.Entity<RentalPoint>()
                .HasMany<Car>(cars => cars.Cars)
                .WithOne(point => point.RentalPoint)
                .HasForeignKey(rpId => rpId.RentalPointId);

            modelBuilder.Entity<Location>()
                .HasOne(rp => rp.RentalPoint)
                .WithOne(loc => loc.Location)
                .HasForeignKey<RentalPoint>(locId => locId.LocationId);

            modelBuilder.Entity<City>()
                .HasMany(l => l.Locations)
                .WithOne(c => c.City)
                .HasForeignKey(c => c.CityId);
        }
    }
}
