using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DAL.EntityConfiguration
{
    public class CarBrandEntityConfiguration : IEntityTypeConfiguration<CarBrandEntity>
    {
        public void Configure(EntityTypeBuilder<CarBrandEntity> builder)
        {
            builder
                .HasKey(cb => cb.Id);
            builder
                .HasMany(c => c.Cars)
                .WithOne(b => b.Brand)
                .HasForeignKey(b => b.BrandId);
            builder
                .Property(cb => cb.Name)
                .IsRequired();
        }
    }
}
