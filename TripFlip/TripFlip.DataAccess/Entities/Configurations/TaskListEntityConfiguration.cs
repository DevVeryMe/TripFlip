using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TripFlip.DataAccess.Entities.Configurations
{
    class TaskListEntityConfiguration : IEntityTypeConfiguration<TaskListEntity>
    {
        public void Configure(EntityTypeBuilder<TaskListEntity> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.Title).IsRequired().HasMaxLength(100);

            builder.HasOne<RouteEntity>(i => i.Route)
                .WithMany(r => r.TaskLists)
                .HasForeignKey(i => i.RouteId);
        }
    }
}
