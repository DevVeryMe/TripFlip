using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TripFlip.DataAccess.Entities.Configurations
{
    class ItemListEntityConfiguration : IEntityTypeConfiguration<ItemListEntity>
    {
        public void Configure(EntityTypeBuilder<ItemListEntity> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Property(i => i.Title).HasMaxLength(100).IsUnicode().IsRequired();

            builder.HasOne<RouteEntity>(i => i.Route)
                .WithMany(r => r.ItemLists)
                .HasForeignKey(i => i.RouteId);
        }
    }
}
