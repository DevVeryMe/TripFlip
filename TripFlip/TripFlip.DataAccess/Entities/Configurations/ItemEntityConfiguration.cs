using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TripFlip.DataAccess.Entities.Configurations
{
    class ItemEntityConfiguration : IEntityTypeConfiguration<ItemEntity>
    {
        public void Configure(EntityTypeBuilder<ItemEntity> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Property(i => i.Title).HasMaxLength(100).IsUnicode().IsRequired();
            builder.Property(i => i.Comment).HasMaxLength(250).IsUnicode().IsRequired(false);
            builder.Property(i => i.Quantity).HasMaxLength(50).IsUnicode().IsRequired(false);
            builder.Property(i => i.IsCompleted).HasDefaultValue(false);
        }
    }
}
