using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DAL.EntityConfiguration
{
    public class BookingReportEntityConfiguration : IEntityTypeConfiguration<BookingReportEntity>
    {
        public void Configure(EntityTypeBuilder<BookingReportEntity> builder)
        {
            builder
                .HasKey(br => br.Id);
            builder
                .Property(br => br.Status)
                .IsRequired();
        }
    }
}