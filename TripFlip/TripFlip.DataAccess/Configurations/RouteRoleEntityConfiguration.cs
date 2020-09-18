using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripFlip.Domain.Entities;

namespace TripFlip.DataAccess.Configurations
{
    class RouteRoleEntityConfiguration : IEntityTypeConfiguration<RouteRoleEntity>
    {
        public void Configure(EntityTypeBuilder<RouteRoleEntity> builder)
        {
            builder.HasKey(routeRole => routeRole.Id);
            builder.Property(routeRole => routeRole.Id).ValueGeneratedOnAdd();

            builder.Property(routeRole => routeRole.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
