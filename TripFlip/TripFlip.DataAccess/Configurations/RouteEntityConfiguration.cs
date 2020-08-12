using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripFlip.Domain.Entities.Entities;

namespace TripFlip.DataAccess.Configurations
{
    public class RouteEntityConfiguration : IEntityTypeConfiguration<RouteEntity>
    {
        public void Configure(EntityTypeBuilder<RouteEntity> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Title).IsRequired().HasMaxLength(100);

            builder.HasOne<TripEntity>(r => r.Trip).
                WithMany(t => t.Routes).
                HasForeignKey(r => r.TripId);
        }
    }
}

