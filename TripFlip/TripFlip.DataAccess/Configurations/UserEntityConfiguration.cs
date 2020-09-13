using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripFlip.Domain.Entities;

namespace TripFlip.DataAccess.Configurations
{
    class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(userEntity => userEntity.Id);
            builder.Property(userEntity => userEntity.Id).ValueGeneratedOnAdd();

            builder.Property(userEntity => userEntity.Email)
                .IsRequired()
                .HasMaxLength(320);
            builder.Property(userEntity => userEntity.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);

            builder
                .HasIndex(user => user.Email)
                .IsUnique();

            builder.Property(userEntity => userEntity.DateCreated)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            builder.Property(userEntity => userEntity.FirstName)
                .HasMaxLength(50);

            builder.Property(userEntity => userEntity.LastName)
                .HasMaxLength(50);

            builder.Property(userEntity => userEntity.AboutMe)
                .HasMaxLength(300);
        }
    }
}
