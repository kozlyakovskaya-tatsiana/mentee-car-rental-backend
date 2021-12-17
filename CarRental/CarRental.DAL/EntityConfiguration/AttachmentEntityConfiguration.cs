using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DAL.EntityConfiguration
{
    public class AttachmentEntityConfiguration : IEntityTypeConfiguration<AttachmentEntity>
    {
        public void Configure(EntityTypeBuilder<AttachmentEntity> builder)
        {
            builder
                .HasKey(a => a.Id);
        }
    }
}
