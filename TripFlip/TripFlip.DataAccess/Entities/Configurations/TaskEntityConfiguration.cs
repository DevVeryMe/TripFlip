using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripFlip.DataAccess.Entities.Enums;

namespace TripFlip.DataAccess.Entities.Configurations
{
    class TaskEntityConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.Description).HasMaxLength(250).IsUnicode().IsRequired();
            builder.Property(t => t.isCompleted).HasDefaultValue(false);
            builder.Property(t => t.PriorityLevel).HasDefaultValue(TaskPriorityLevel.Low);
        }
    }
}
