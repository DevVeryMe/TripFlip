using Microsoft.EntityFrameworkCore;
using TripFlip.DataAccess.Entities;
using TripFlip.DataAccess.Entities.Configurations;

namespace TripFlip.DataAccess
{
    public class FlipTripDbContext : DbContext
    {
        public DbSet<ItemEntity> Items { get; set; }

        public DbSet<ItemListEntity> ItemLists { get; set; }

        public DbSet<RouteEntity> Routes { get; set; }

        public DbSet<RoutePointEntity> RoutePoints { get; set; }

        public DbSet<TaskEntity> Tasks { get; set; }

        public DbSet<TaskListEntity> TaskLists { get; set; }

        public DbSet<TripEntity> Trips { get; set; }

        public DbSet<TripFileEntity> TripFiles { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Constants.MsSqlLocalDbConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RouteEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoutePointEntityConfiguration());
        }
    }
}
