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
            builder.Property(t => t.Title).HasMaxLength(100).IsUnicode().IsRequired();
        }
    }
}
