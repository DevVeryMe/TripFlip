using GoogleAuthentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoogleAuthentication.DataAccess.Configurations
{
    internal class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(userEntity => userEntity.Id);
            builder.Property(userEntity => userEntity.Id).ValueGeneratedOnAdd();

            builder.Property(userEntity => userEntity.Email)
                .IsRequired()
                .HasMaxLength(320);

            builder.Property(userEntity => userEntity.PasswordHash)
                .IsRequired(false)
                .HasMaxLength(255);

            builder
                .HasIndex(user => user.Email)
                .IsUnique();
        }

    }
}
