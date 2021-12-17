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

            modelBuilder.Entity<CarEntity>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<CarEntity>()
                .HasMany(r => r.Reports)
                .WithOne(c => c.Car)
                .HasForeignKey(c => c.CarId);
            modelBuilder.Entity<CarEntity>()
                .HasMany(a => a.Photos)
                .WithOne(c => c.Car)
                .HasForeignKey(c => c.CarId);

            modelBuilder.Entity<CarBrandEntity>()
                .HasKey(cb => cb.Id);
            modelBuilder.Entity<CarBrandEntity>()
                .HasMany(c => c.Cars)
                .WithOne(b => b.Brand)
                .HasForeignKey(b => b.BrandId);
            modelBuilder.Entity<CarBrandEntity>()
                .Property(cb => cb.Name)
                .IsRequired();

            modelBuilder.Entity<RentalPointEntity>()
                .HasKey(rp => rp.Id);
            modelBuilder.Entity<RentalPointEntity>()
                .HasMany(c => c.Cars)
                .WithOne(p => p.RentalPoint)
                .HasForeignKey(p => p.RentalPointId);
            modelBuilder.Entity<RentalPointEntity>()
                .Property(rp => rp.Name)
                .IsRequired();

            modelBuilder.Entity<LocationEntity>()
                .HasKey(l => l.Id);
            modelBuilder.Entity<LocationEntity>()
                .HasOne(rp => rp.RentalPoint)
                .WithOne(l => l.Location)
                .HasForeignKey<RentalPointEntity>(l => l.LocationId);
            modelBuilder.Entity<LocationEntity>()
                .Property(l => l.Address)
                .IsRequired();

            modelBuilder.Entity<CityEntity>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<CityEntity>()
                .HasMany(l => l.Locations)
                .WithOne(c => c.City)
                .HasForeignKey(c => c.CityId);
            modelBuilder.Entity<CityEntity>()
                .Property(c => c.Name)
                .IsRequired();

            modelBuilder.Entity<CountryEntity>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<CountryEntity>()
                .HasMany(ci => ci.Cities)
                .WithOne(co => co.Country)
                .HasForeignKey(co => co.CountryId);
            modelBuilder.Entity<CountryEntity>()
                .Property(c => c.Name)
                .IsRequired();

            modelBuilder.Entity<BookingReportEntity>()
                .HasKey(br => br.Id);
            modelBuilder.Entity<BookingReportEntity>()
                .Property(br => br.Status)
                .IsRequired();

            modelBuilder.Entity<AttachmentEntity>()
                .HasKey(a => a.Id);
        }
    }
}