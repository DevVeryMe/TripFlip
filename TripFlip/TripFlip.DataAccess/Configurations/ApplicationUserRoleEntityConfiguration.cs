using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripFlip.Domain.Entities;

namespace TripFlip.DataAccess.Configurations
{
    class ApplicationUserRoleEntityConfiguration
        : IEntityTypeConfiguration<ApplicationUserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRoleEntity> builder)
        {
            builder.HasKey(applicationUserRole => new
            {
                applicationUserRole.UserId,
                applicationUserRole.ApplicationRoleId
            });

            builder.HasOne(applicationUserRole => applicationUserRole.User)
                .WithMany(user => user.ApplicationRoles)
                .HasForeignKey(applicationUserRole => applicationUserRole.UserId);

            builder.HasOne(applicationUserRole => applicationUserRole.ApplicationRole)
                .WithMany(appRole => appRole.Users)
                .HasForeignKey(applicationUserRole => applicationUserRole.ApplicationRoleId);
        }
    }
}
