using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripFlip.Domain.Entities;
using TripFlip.Domain.Entities.Enums;

namespace TripFlip.DataAccess.Configurations
{
    class TaskEntityConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.Description).IsRequired().HasMaxLength(250);
            builder.Property(t => t.isCompleted).HasDefaultValue(false);
            builder.Property(t => t.PriorityLevel).HasDefaultValue(TaskPriorityLevel.Low);

            builder.HasOne<TaskListEntity>(t => t.TaskList)
                .WithMany(t => t.Tasks)
                .HasForeignKey(t => t.TaskListId);
        }
    }
}
