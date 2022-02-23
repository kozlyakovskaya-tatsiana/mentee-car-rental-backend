using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DAL.EntityConfiguration
{
    public class RentalPointEntityConfiguration : IEntityTypeConfiguration<RentalPointEntity>
    {
        public void Configure(EntityTypeBuilder<RentalPointEntity> builder)
        {
            builder
                .HasKey(rp => rp.Id);
            builder
                .HasMany(c => c.Cars)
                .WithOne(p => p.RentalPoint)
                .HasForeignKey(p => p.RentalPointId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .Property(rp => rp.Name)
                .IsRequired();

            builder
                .HasOne(c => c.Location)
                .WithOne(r => r.RentalPoint)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}