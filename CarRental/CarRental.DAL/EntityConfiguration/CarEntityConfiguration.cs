using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DAL.EntityConfiguration
{
    public class CarEntityConfiguration : IEntityTypeConfiguration<CarEntity>
    {
        public void Configure(EntityTypeBuilder<CarEntity> builder)
        {
            builder
                .HasKey(c => c.Id);
            builder
                .HasMany(r => r.Reports)
                .WithOne(c => c.Car)
                .HasForeignKey(c => c.CarId);
            builder
                .HasMany(a => a.Photos)
                .WithOne(c => c.Car)
                .HasForeignKey(c => c.CarId);
        }
    }
}
