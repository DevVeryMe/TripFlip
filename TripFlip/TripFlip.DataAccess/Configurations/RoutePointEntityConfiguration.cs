using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripFlip.Domain.Entities;

namespace TripFlip.DataAccess.Configurations
{
    public class RoutePointEntityConfiguration : IEntityTypeConfiguration<RoutePointEntity>
    {
        public void Configure(EntityTypeBuilder<RoutePointEntity> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.DateCreated).HasDefaultValueSql("SYSDATETIMEOFFSET()");

            builder.HasOne<RouteEntity>(p => p.Route).
                WithMany(r => r.RoutePoints).
                HasForeignKey(p => p.RouteId);
        }
    }
}
