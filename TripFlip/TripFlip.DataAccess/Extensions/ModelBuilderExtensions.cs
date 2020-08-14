using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using TripFlip.Domain.Entities;

namespace TripFlip.DataAccess.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TripEntity>().HasData(new TripEntity()
            {
                Id = 1,
                Title = "Our first trip",
                Description = "We wanna visit several different cities of Ukraine",
                DateCreated = DateTimeOffset.Now,
                StartsAt = DateTimeOffset.Parse("17/08/2020 14:00:00"),
                EndsAt = DateTimeOffset.Parse("20/08/2020 19:00:00")
            });

            modelBuilder.Entity<RouteEntity>().HasData(
                new RouteEntity()
                {
                    Id = 1,
                    TripId = 1,
                    Title = "First route",
                    DateCreated = DateTimeOffset.Now
                },

                new RouteEntity()
                {
                    Id = 2,
                    TripId = 1,
                    Title = "Second route",
                    DateCreated = DateTimeOffset.Now
                });

            modelBuilder.Entity<RoutePointEntity>().HasData(
                new RoutePointEntity() {Id = 1, RouteId = 1, Longitude = 14.333, Latitude = 56.642, Order = 1, DateCreated = DateTimeOffset.Now},
                new RoutePointEntity() {Id = 2, RouteId = 1, Longitude = 17.332, Latitude = 60.341, Order = 2, DateCreated = DateTimeOffset.Now },
                new RoutePointEntity() {Id = 3, RouteId = 1, Longitude = 18.199, Latitude = 62.622, Order = 3, DateCreated = DateTimeOffset.Now },
                new RoutePointEntity() {Id = 4, RouteId = 1, Longitude = 22.144, Latitude = 70.849, Order = 4, DateCreated = DateTimeOffset.Now },
                new RoutePointEntity() {Id = 5, RouteId = 1, Longitude = 31.122, Latitude = 97.787, Order = 5, DateCreated = DateTimeOffset.Now },
                new RoutePointEntity() { Id = 6, RouteId = 2, Longitude = 49.523, Latitude = 118.782, Order = 1, DateCreated = DateTimeOffset.Now },
                new RoutePointEntity() { Id = 7, RouteId = 2, Longitude = 54.321, Latitude = 145.899, Order = 2, DateCreated = DateTimeOffset.Now },
                new RoutePointEntity() { Id = 8, RouteId = 2, Longitude = 69.213, Latitude = 160.998, Order = 3, DateCreated = DateTimeOffset.Now },
                new RoutePointEntity() { Id = 9, RouteId = 2, Longitude = 71.294, Latitude = 180.111, Order = 4, DateCreated = DateTimeOffset.Now },
                new RoutePointEntity() { Id = 10, RouteId = 2, Longitude = 73.225, Latitude = 185.235, Order = 5, DateCreated = DateTimeOffset.Now });

            modelBuilder.Entity<ItemListEntity>().HasData(
                new ItemListEntity()
                {
                    Id = 1,
                    RouteId = 1,
                    Title = "Most needed items",
                    DateCreated = DateTimeOffset.Now
                },
                new ItemListEntity()
                {
                    Id = 2,
                    RouteId = 1,
                    Title = "Not needed, but you can take",
                    DateCreated = DateTimeOffset.Now,
                });

            modelBuilder.Entity<ItemEntity>().HasData(
                new ItemEntity() { Id = 1, ItemListId = 1, Title = "Id card" },
                new ItemEntity() { Id = 2, ItemListId = 1, Title = "Money", Quantity = "1000$" },
                new ItemEntity() { Id = 3, ItemListId = 1, Title = "Train tickets" },
                new ItemEntity() { Id = 4, ItemListId = 2, Title = "Playing cards" },
                new ItemEntity() { Id = 5, ItemListId = 2, Title = "Food" },
                new ItemEntity() { Id = 6, ItemListId = 2, Title = "Guitar" }
            );

            modelBuilder.Entity<TaskListEntity>().HasData(
                new TaskListEntity(){
                    Id = 1,
                    RouteId = 1,
                    Title = "Tasks",
                    DateCreated = DateTimeOffset.Now
                });

            modelBuilder.Entity<TaskEntity>().HasData(
                new TaskEntity() { Id = 1, TaskListId = 1, DateCreated = DateTimeOffset.Now, Description = "Buy food." },
                new TaskEntity() { Id = 2, TaskListId = 1, DateCreated = DateTimeOffset.Now, Description = "Buy train tickets" },
                new TaskEntity() { Id = 3, TaskListId = 1, DateCreated = DateTimeOffset.Now, Description = "Buy tent" },
                new TaskEntity() { Id = 4, TaskListId = 1, DateCreated = DateTimeOffset.Now, Description = "Buy drugs" },
                new TaskEntity() { Id = 5, TaskListId = 1, DateCreated = DateTimeOffset.Now, Description = "Buy chips" });
        }
    }
}
