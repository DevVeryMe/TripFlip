﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using TripFlip.Domain.Entities;
using TripFlip.Domain.Entities.Enums;

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
                DateCreated = DateTimeOffset.Parse("17/08/2020 14:00:00",
                    CultureInfo.GetCultureInfo("en-GB").DateTimeFormat),
                StartsAt = DateTimeOffset.Parse("17/08/2020 14:00:00", 
                    CultureInfo.GetCultureInfo("en-GB").DateTimeFormat),
                EndsAt = DateTimeOffset.Parse("20/08/2020 19:00:00",
                    CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
            });

            modelBuilder.Entity<RouteEntity>().HasData(
                new RouteEntity()
                {
                    Id = 1,
                    TripId = 1,
                    Title = "First route",
                    DateCreated = DateTimeOffset.Parse("17/08/2020 14:00:00",
                        CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
                },

                new RouteEntity()
                {
                    Id = 2,
                    TripId = 1,
                    Title = "Second route",
                    DateCreated = DateTimeOffset.Parse("17/08/2020 14:00:00",
                        CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
                });

            modelBuilder.Entity<RoutePointEntity>().HasData(
                new RoutePointEntity() {Id = 1, RouteId = 1, Longitude = 14.333, Latitude = 56.642, Order = 1, DateCreated = DateTimeOffset.Parse("17/08/2020 14:00:00",
                    CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
                },
                new RoutePointEntity() {Id = 2, RouteId = 1, Longitude = 17.332, Latitude = 60.341, Order = 2, DateCreated = DateTimeOffset.Parse("17/08/2020 14:00:00",
                    CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
                },
                new RoutePointEntity() {Id = 3, RouteId = 1, Longitude = 18.199, Latitude = 62.622, Order = 3, DateCreated = DateTimeOffset.Parse("17/08/2020 14:00:00",
                    CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
                },
                new RoutePointEntity() {Id = 4, RouteId = 1, Longitude = 22.144, Latitude = 70.849, Order = 4, DateCreated = DateTimeOffset.Parse("17/08/2020 14:00:00",
                    CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
                },
                new RoutePointEntity() {Id = 5, RouteId = 1, Longitude = 31.122, Latitude = 97.787, Order = 5, DateCreated = DateTimeOffset.Parse("17/08/2020 14:00:00",
                    CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
                },
                new RoutePointEntity() { Id = 6, RouteId = 2, Longitude = 49.523, Latitude = 118.782, Order = 1, DateCreated = DateTimeOffset.Parse("17/08/2020 14:00:00",
                    CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
                },
                new RoutePointEntity() { Id = 7, RouteId = 2, Longitude = 54.321, Latitude = 145.899, Order = 2, DateCreated = DateTimeOffset.Parse("17/08/2020 14:00:00",
                    CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
                },
                new RoutePointEntity() { Id = 8, RouteId = 2, Longitude = 69.213, Latitude = 160.998, Order = 3, DateCreated = DateTimeOffset.Parse("17/08/2020 14:00:00",
                    CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
                },
                new RoutePointEntity() { Id = 9, RouteId = 2, Longitude = 71.294, Latitude = 180.111, Order = 4, DateCreated = DateTimeOffset.Parse("17/08/2020 14:00:00",
                    CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
                },
                new RoutePointEntity() { Id = 10, RouteId = 2, Longitude = 73.225, Latitude = 185.235, Order = 5, DateCreated = DateTimeOffset.Parse("17/08/2020 14:00:00",
                    CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
                });

            modelBuilder.Entity<ItemListEntity>().HasData(
                new ItemListEntity()
                {
                    Id = 1,
                    RouteId = 1,
                    Title = "Most needed items",
                    DateCreated = DateTimeOffset.Parse("17/08/2020 14:00:00",
                        CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
                },
                new ItemListEntity()
                {
                    Id = 2,
                    RouteId = 1,
                    Title = "Not needed, but you can take",
                    DateCreated = DateTimeOffset.Parse("17/08/2020 14:00:00",
                        CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
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
                new TaskEntity() { Id = 1, TaskListId = 1, DateCreated = DateTimeOffset.Parse("17/08/2020 14:00:00",
                    CultureInfo.GetCultureInfo("en-GB").DateTimeFormat), Description = "Buy food." },
                new TaskEntity() { Id = 2, TaskListId = 1, DateCreated = DateTimeOffset.Parse("17/08/2020 14:00:00",
                    CultureInfo.GetCultureInfo("en-GB").DateTimeFormat), Description = "Buy train tickets" },
                new TaskEntity() { Id = 3, TaskListId = 1, DateCreated = DateTimeOffset.Parse("17/08/2020 14:00:00",
                    CultureInfo.GetCultureInfo("en-GB").DateTimeFormat), Description = "Buy tent" },
                new TaskEntity() { Id = 4, TaskListId = 1, DateCreated = DateTimeOffset.Parse("17/08/2020 14:00:00",
                    CultureInfo.GetCultureInfo("en-GB").DateTimeFormat), Description = "Buy drugs" },
                new TaskEntity() { Id = 5, TaskListId = 1, DateCreated = DateTimeOffset.Parse("17/08/2020 14:00:00",
                    CultureInfo.GetCultureInfo("en-GB").DateTimeFormat), Description = "Buy chips" });

            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity()
                {
                    Id = new Guid("e7cd790b-7dfb-4fb2-bba8-d01a65b39621"),
                    Email = "sample1.email@mail.com",
                    PasswordHash = "some_hash",
                    DateCreated = DateTimeOffset.Parse("17/08/2020 14:00:00",
                        CultureInfo.GetCultureInfo("en-GB").DateTimeFormat),
                    FirstName = "Andrew",
                    LastName = "Kravchuk",
                    AboutMe = "About me",
                    Gender = UserGender.Male,
                    BirthDate = DateTimeOffset.Parse("17/08/2020 14:00:00",
                        CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
                },
                new UserEntity()
                {
                    Id = new Guid("f755f10f-85ad-4fa2-9bfe-8d43c8d94aa5"),
                    Email = "sample2.email@mail.com",
                    PasswordHash = "some_other_hash",
                    DateCreated = DateTimeOffset.Parse("17/08/2020 14:00:00",
                        CultureInfo.GetCultureInfo("en-GB").DateTimeFormat),
                    FirstName = "Andrew",
                    LastName = "Veremiy",
                    AboutMe = "About me",
                    Gender = UserGender.Male,
                    BirthDate = DateTimeOffset.Parse("17/08/2020 14:00:00",
                        CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
                },
                new UserEntity()
                {
                    Id = new Guid("8f03d642-e4b1-4d34-a3bd-4467ecdfd01b"),
                    Email = "sample3.email@mail.com",
                    PasswordHash = "hash_hash",
                    DateCreated = DateTimeOffset.Parse("17/08/2020 14:00:00",
                        CultureInfo.GetCultureInfo("en-GB").DateTimeFormat),
                    FirstName = "Stas",
                    LastName = "Lazarev",
                    AboutMe = "About me",
                    Gender = UserGender.Male,
                    BirthDate = DateTimeOffset.Parse("17/08/2020 14:00:00",
                        CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
                });

            modelBuilder.Entity<TripRoleEntity>().HasData(
                new TripRoleEntity()
                {
                    Id = 1,
                    Name = "Admin"
                },
                new TripRoleEntity()
                {
                    Id = 2,
                    Name = "Editor"
                },
                new TripRoleEntity()
                {
                    Id = 3,
                    Name = "Guest"
                });

            modelBuilder.Entity<RouteRoleEntity>().HasData(
                new RouteRoleEntity()
                {
                    Id = 1,
                    Name = "Admin"
                },
                new RouteRoleEntity()
                {
                    Id = 2,
                    Name = "Editor"
                });

            modelBuilder.Entity<ApplicationRoleEntity>().HasData(
                new ApplicationRoleEntity()
                {
                    Id = 1,
                    Name = "SuperAdmin"
                },
                new ApplicationRoleEntity()
                {
                    Id = 2,
                    Name = "Admin"
                });
        }
    }
}
