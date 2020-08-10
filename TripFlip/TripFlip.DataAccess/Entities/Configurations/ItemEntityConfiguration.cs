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
            builder.Property(i => i.Title).IsRequired().HasMaxLength(100);
            builder.Property(i => i.Comment).IsRequired(false).HasMaxLength(250);
            builder.Property(i => i.Quantity).IsRequired(false).HasMaxLength(50);
            builder.Property(i => i.IsCompleted).HasDefaultValue(false);

            builder.HasOne<ItemListEntity>(i => i.ItemList)
                .WithMany(i => i.Items)
                .HasForeignKey(i => i.ItemListId);
        }
    }
}
