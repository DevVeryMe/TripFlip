using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TripFlip.DataAccess.Entities.Configurations
{
    public class RoutePointEntityConfiguration : IEntityTypeConfiguration<RoutePointEntity>
    {
        public void Configure(EntityTypeBuilder<RoutePointEntity> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasOne<RouteEntity>(p => p.Route).
                WithMany(r => r.RoutePoints).
                HasForeignKey(p => p.RouteId);
        }
    }
}
