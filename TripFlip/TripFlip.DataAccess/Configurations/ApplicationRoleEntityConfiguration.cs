using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripFlip.Domain.Entities;

namespace TripFlip.DataAccess.Configurations
{
    class ApplicationRoleEntityConfiguration : IEntityTypeConfiguration<ApplicationRoleEntity>
    {
        public void Configure(EntityTypeBuilder<ApplicationRoleEntity> builder)
        {
            builder.HasKey(appRole => appRole.Id);
            builder.Property(appRole => appRole.Id).ValueGeneratedOnAdd();

            builder.Property(appRole => appRole.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
