using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DAL.EntityConfiguration
{
    public class CountryEntityConfiguration : IEntityTypeConfiguration<CountryEntity>
    {
        public void Configure(EntityTypeBuilder<CountryEntity> builder)
        {
            builder
                .HasKey(c => c.Id);
            builder
                .HasMany(ci => ci.Cities)
                .WithOne(co => co.Country)
                .HasForeignKey(co => co.CountryId);
            builder
                .Property(c => c.Name)
                .IsRequired();
        }
    }
}
