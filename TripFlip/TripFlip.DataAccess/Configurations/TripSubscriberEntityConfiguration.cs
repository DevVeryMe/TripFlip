using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripFlip.Domain.Entities;

namespace TripFlip.DataAccess.Configurations
{
    class TripSubscriberEntityConfiguration
        : IEntityTypeConfiguration<TripSubscriberEntity>
    {
        public void Configure(EntityTypeBuilder<TripSubscriberEntity> builder)
        {
            builder.HasKey(tripSubscriber => tripSubscriber.Id);
            builder.Property(tripSubscriber => tripSubscriber.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(tripSubscriber => tripSubscriber.User)
                .WithMany(user => user.TripSubscriptions)
                .HasForeignKey(tripSubscriber => tripSubscriber.UserId);

            builder.HasOne(tripSubscriber => tripSubscriber.Trip)
                .WithMany(trip => trip.TripSubscribers)
                .HasForeignKey(tripSubscriber => tripSubscriber.TripId);

            builder.Property(tripSubscriber => tripSubscriber.DateSubscribed)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");
        }
    }
}
