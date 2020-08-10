using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TripFlip.DataAccess.Entities.Configurations
{
    class TripFileEntityConfiguration : IEntityTypeConfiguration<TripFileEntity>
    {
        public void Configure(EntityTypeBuilder<TripFileEntity> builder)
        {
            builder.HasKey(tripFile => tripFile.Id);
            builder.Property(tripFile => tripFile.Id).ValueGeneratedOnAdd();
            builder.Property(tripFile => tripFile.FileUrl).IsRequired();
            builder.Property(tripFile => tripFile.Title).HasMaxLength(100);

            builder
                .HasOne(tripFile => tripFile.Trip)
                .WithMany(trip => trip.TripFiles);
        }
    }
}
