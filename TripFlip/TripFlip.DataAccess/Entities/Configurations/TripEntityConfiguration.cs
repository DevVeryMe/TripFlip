using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripFlip.Domain.Entities.Entities;

namespace TripFlip.DataAccess.Entities.Configurations
{
    class TripEntityConfiguration : IEntityTypeConfiguration<TripEntity>
    {
        public void Configure(EntityTypeBuilder<TripEntity> builder)
        {
            builder.HasKey(trip => trip.Id);
            builder.Property(trip => trip.Id).ValueGeneratedOnAdd();

            builder.Property(trip => trip.Title)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(trip => trip.Description).HasMaxLength(500);
            builder.Property(trip => trip.StartsAt).IsRequired(false);
            builder.Property(trip => trip.EndsAt).IsRequired(false);
        }
    }
}
