using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripFlip.Domain.Entities;

namespace TripFlip.DataAccess.Configurations
{
    class ItemAssigneeEntityConfiguration
        : IEntityTypeConfiguration<ItemAssigneeEntity>
    {
        public void Configure(EntityTypeBuilder<ItemAssigneeEntity> builder)
        {
            builder.HasKey(itemAssignee => new
            {
                itemAssignee.ItemId,
                itemAssignee.RouteSubscriberId
            });

            builder.HasOne(itemAssignee => itemAssignee.Item)
                .WithMany(item => item.ItemAssignees)
                .HasForeignKey(itemAssignee => itemAssignee.ItemId);

            builder.HasOne(itemAssignee => itemAssignee.RouteSubscriber)
                .WithMany(routeSubscriber => routeSubscriber.AssignedItems)
                .HasForeignKey(itemAssignee => itemAssignee.RouteSubscriberId);
        }
    }
}
