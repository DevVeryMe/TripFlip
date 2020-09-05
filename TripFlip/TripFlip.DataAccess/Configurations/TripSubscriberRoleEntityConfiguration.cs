using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripFlip.Domain.Entities;

namespace TripFlip.DataAccess.Configurations
{
    class TripSubscriberRoleEntityConfiguration
        : IEntityTypeConfiguration<TripSubscriberRoleEntity>
    {
        public void Configure(EntityTypeBuilder<TripSubscriberRoleEntity> builder)
        {
            builder.HasKey(tripSubscriberRole => new
            {
                tripSubscriberRole.TripSubscriberId,
                tripSubscriberRole.TripRoleId
            });

            builder.HasOne(tripSubscriberRole => tripSubscriberRole.TripSubscriber)
                .WithMany(tripSubscriber => tripSubscriber.TripRoles)
                .HasForeignKey(tripSubscriberRole => tripSubscriberRole.TripSubscriberId);

            builder.HasOne(tripSubscriberRole => tripSubscriberRole.TripRole)
                .WithMany(tripRole => tripRole.TripSubscribers)
                .HasForeignKey(tripSubscriberRole => tripSubscriberRole.TripRoleId);
        }
    }
}
