using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripFlip.Domain.Entities;

namespace TripFlip.DataAccess.Configurations
{
    class ItemListEntityConfiguration : IEntityTypeConfiguration<ItemListEntity>
    {
        public void Configure(EntityTypeBuilder<ItemListEntity> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Property(i => i.Title).IsRequired().HasMaxLength(100);

            builder.HasOne<RouteEntity>(i => i.Route)
                .WithMany(r => r.ItemLists)
                .HasForeignKey(i => i.RouteId);
        }
    }
}
