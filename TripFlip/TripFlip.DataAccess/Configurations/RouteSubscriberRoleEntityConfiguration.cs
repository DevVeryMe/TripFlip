using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripFlip.Domain.Entities;

namespace TripFlip.DataAccess.Configurations
{
    class RouteSubscriberRoleEntityConfiguration
        : IEntityTypeConfiguration<RouteSubscriberRoleEntity>
    {
        public void Configure(EntityTypeBuilder<RouteSubscriberRoleEntity> builder)
        {
            builder.HasKey(routeSubscriberRole => new
            {
                routeSubscriberRole.RouteSubscriberId,
                routeSubscriberRole.RouteRoleId
            });

            builder.HasOne(routeSubscriberRole => routeSubscriberRole.RouteSubscriber)
                .WithMany(routeSubscriber => routeSubscriber.RouteRoles)
                .HasForeignKey(routeSubscriberRole => routeSubscriberRole.RouteSubscriberId);

            builder.HasOne(routeSubscriberRole => routeSubscriberRole.RouteRole)
                .WithMany(routeRole => routeRole.RouteSubscribers)
                .HasForeignKey(routeSubscriberRole => routeSubscriberRole.RouteRoleId);
        }
    }
}
