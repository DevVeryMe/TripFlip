using GoogleAuthentication.DataAccess.Configurations;
using GoogleAuthentication.DataAccess.Extensions;
using GoogleAuthentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoogleAuthentication.DataAccess
{
    public class GoogleAuthenticationDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public GoogleAuthenticationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());

            modelBuilder.Seed();
        }
    }
}
