using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripFlip.Domain.Entities;

namespace TripFlip.DataAccess.Configurations
{
    public class RouteEntityConfiguration : IEntityTypeConfiguration<RouteEntity>
    {
        public void Configure(EntityTypeBuilder<RouteEntity> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.Title).IsRequired().HasMaxLength(100);
            builder.Property(r => r.DateCreated).HasDefaultValueSql("SYSDATETIMEOFFSET()");

            builder.HasOne<TripEntity>(r => r.Trip).
                WithMany(t => t.Routes).
                HasForeignKey(r => r.TripId);
        }
    }
}

