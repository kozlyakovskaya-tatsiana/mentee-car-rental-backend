using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.EFCore
{
    public sealed class CarRentalDbContext : DbContext
    {
        public CarRentalDbContext()
            :base()
        { }

        public DbSet<CarBrand> CarBrands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<BookingReport> Reports { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<RentalPoint> RentalPoints { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TestDB;Username=postgres;Password=root;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarRentalDbContext).Assembly);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<User>()
                .HasMany<BookingReport>(r => r.Reports)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);
            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .IsRequired();

            modelBuilder.Entity<Car>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Car>()
                .HasMany<BookingReport>(r => r.Reports)
                .WithOne(c => c.Car)
                .HasForeignKey(c => c.CarId);
            modelBuilder.Entity<Car>()
                .HasMany<Attachment>(a => a.Photos)
                .WithOne(c => c.Car)
                .HasForeignKey(c => c.CarId);

            modelBuilder.Entity<CarBrand>()
                .HasKey(cb => cb.Id);
            modelBuilder.Entity<CarBrand>()
                .HasMany<Car>(c => c.Cars)
                .WithOne(b => b.Brand)
                .HasForeignKey(b => b.BrandId);
            modelBuilder.Entity<CarBrand>()
                .Property(cb => cb.Name)
                .IsRequired();

            modelBuilder.Entity<RentalPoint>()
                .HasKey(rp => rp.Id);
            modelBuilder.Entity<RentalPoint>()
                .HasMany<Car>(c => c.Cars)
                .WithOne(p => p.RentalPoint)
                .HasForeignKey(p => p.RentalPointId);
            modelBuilder.Entity<RentalPoint>()
                .Property(rp => rp.Name)
                .IsRequired();

            modelBuilder.Entity<Location>()
                .HasKey(l => l.Id);
            modelBuilder.Entity<Location>()
                .HasOne(rp => rp.RentalPoint)
                .WithOne(l => l.Location)
                .HasForeignKey<RentalPoint>(l => l.LocationId);
            modelBuilder.Entity<Location>()
                .Property(l => l.Address)
                .IsRequired();

            modelBuilder.Entity<City>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<City>()
                .HasMany(l => l.Locations)
                .WithOne(c => c.City)
                .HasForeignKey(c => c.CityId);
            modelBuilder.Entity<City>()
                .Property(c => c.Name)
                .IsRequired();

            modelBuilder.Entity<Country>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Country>()
                .HasMany(ci => ci.Cities)
                .WithOne(co => co.Country)
                .HasForeignKey(co => co.CountryId);
            modelBuilder.Entity<Country>()
                .Property(c => c.Name)
                .IsRequired();

            modelBuilder.Entity<BookingReport>()
                .HasKey(br => br.Id);
            modelBuilder.Entity<BookingReport>()
                .Property(br => br.Status)
                .IsRequired();

            modelBuilder.Entity<Attachment>()
                .HasKey(a => a.Id);
        }

    }
}