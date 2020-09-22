using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripFlip.Domain.Entities;

namespace TripFlip.DataAccess.Configurations
{
    class TaskAssigneeEntityConfiguration
        : IEntityTypeConfiguration<TaskAssigneeEntity>
    {
        public void Configure(EntityTypeBuilder<TaskAssigneeEntity> builder)
        {
            builder.HasKey(taksAssignee => new
            {
                taksAssignee.TaskId,
                taksAssignee.RouteSubscriberId
            });

            builder.HasOne(taksAssignee => taksAssignee.Task)
                .WithMany(task => task.TaskAssignees)
                .HasForeignKey(taksAssignee => taksAssignee.TaskId);

            builder.HasOne(taksAssignee => taksAssignee.RouteSubscriber)
                .WithMany(routeSubscriber => routeSubscriber.AssignedTasks)
                .HasForeignKey(taksAssignee => taksAssignee.RouteSubscriberId);
        }
    }
}
