using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DAL.EntityConfiguration
{
    public class LocationEntityConfiguration : IEntityTypeConfiguration<LocationEntity>
    {
        public void Configure(EntityTypeBuilder<LocationEntity> builder)
        {
            builder
                .HasKey(l => l.Id);

            builder
                .HasOne(rp => rp.RentalPoint)
                .WithOne(l => l.Location)
                .HasForeignKey<RentalPointEntity>(l => l.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(c => c.City)
                .WithMany(l => l.Locations)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(l => l.Address)
                .IsRequired();
        }
    }
}