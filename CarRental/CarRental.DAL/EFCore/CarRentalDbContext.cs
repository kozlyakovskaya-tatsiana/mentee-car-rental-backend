﻿using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.EFCore
{
    public sealed class CarRentalDbContext : DbContext
    {
        public CarRentalDbContext()
        {
            Database.EnsureCreated();
        }

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
            optionsBuilder.UseNpgsql("Host=localhost;" +
                                     "Port=5432;Database=postgres;" +
                                     "Username=postgres;" +
                                     "Password=root;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany<BookingReport>(r => r.Reports)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId); //onDelete

            modelBuilder.Entity<Car>()
                .HasMany<BookingReport>(r => r.Reports)
                .WithOne(c => c.Car)
                .HasForeignKey(c => c.CarId);

            modelBuilder.Entity<Car>()
                .HasMany<Attachment>(a => a.Photos)
                .WithOne(c => c.Car)
                .HasForeignKey(c => c.CarId);

            modelBuilder.Entity<CarBrand>()
                .HasMany<Car>(c => c.Cars)
                .WithOne(b => b.Brand)
                .HasForeignKey(b => b.BrandId);

            modelBuilder.Entity<RentalPoint>()
                .HasMany<Car>(c => c.Cars)
                .WithOne(p => p.RentalPoint)
                .HasForeignKey(p => p.RentalPointId);

            modelBuilder.Entity<Location>()
                .HasOne(rp => rp.RentalPoint)
                .WithOne(l => l.Location)
                .HasForeignKey<RentalPoint>(l => l.LocationId);

            modelBuilder.Entity<City>()
                .HasMany(l => l.Locations)
                .WithOne(c => c.City)
                .HasForeignKey(c => c.CityId);

            modelBuilder.Entity<Country>()
                .HasMany(ci => ci.Cities)
                .WithOne(co => co.Country)
                .HasForeignKey(co => co.CountryId);
        }
    }
}
