using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripFlip.Domain.Entities;

namespace TripFlip.DataAccess.Configurations
{
    class RouteSubscriberEntityConfiguration
        : IEntityTypeConfiguration<RouteSubscriberEntity>
    {
        public void Configure(EntityTypeBuilder<RouteSubscriberEntity> builder)
        {
            builder.HasKey(routeSubscriber => routeSubscriber.Id);
            builder.Property(routeSubscriber => routeSubscriber.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(routeSubscriber => routeSubscriber.Route)
                .WithMany(route => route.RouteSubscribers)
                .HasForeignKey(routeSubscriber => routeSubscriber.RouteId);

            builder.HasOne(routeSubscriber => routeSubscriber.TripSubscriber)
                .WithMany(tripSubscriber => tripSubscriber.RouteSubscriptions)
                .HasForeignKey(routeSubscriber => routeSubscriber.TripSubscriberId);

            builder.Property(routeSubscriber => routeSubscriber.DateSubscribed)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");
        }
    }
}
