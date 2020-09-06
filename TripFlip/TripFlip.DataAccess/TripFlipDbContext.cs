using Microsoft.EntityFrameworkCore;
using TripFlip.DataAccess.Configurations;
using TripFlip.DataAccess.Extensions;
using TripFlip.Domain.Entities;

namespace TripFlip.DataAccess
{
    public class TripFlipDbContext : DbContext
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

        public DbSet<TripRoleEntity> TripRoles { get; set; }

        public DbSet<TripSubscriberEntity> TripSubscribers { get; set; }

        public DbSet<TripSubscriberRoleEntity> TripSubscribersRoles { get; set; }

        public TripFlipDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RouteEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoutePointEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ItemListEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TaskEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TaskListEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TripEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TripFileEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TripRoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TripSubscriberEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TripSubscriberRoleEntityConfiguration());

            modelBuilder.Seed();
        }
    }
}
