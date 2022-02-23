using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DAL.EntityConfiguration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder
                .HasKey(u => u.Id);
            builder
                .HasMany(r => r.Reports)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);
            builder
                .Property(u => u.FirstName)
                .IsRequired();
            builder
                .Property(u => u.LastName)
                .IsRequired();
        }
    }
}