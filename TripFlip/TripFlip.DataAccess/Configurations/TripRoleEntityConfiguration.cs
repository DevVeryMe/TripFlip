using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripFlip.Domain.Entities;

namespace TripFlip.DataAccess.Configurations
{
    class TripRoleEntityConfiguration : IEntityTypeConfiguration<TripRoleEntity>
    {
        public void Configure(EntityTypeBuilder<TripRoleEntity> builder)
        {
            builder.HasKey(tripRole => tripRole.Id);
            builder.Property(tripRole => tripRole.Id).ValueGeneratedOnAdd();

            builder.Property(tripRole => tripRole.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
