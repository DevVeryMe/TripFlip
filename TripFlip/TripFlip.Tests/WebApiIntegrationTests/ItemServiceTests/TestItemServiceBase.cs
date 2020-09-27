﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Moq;
using TripFlip.Domain.Entities;
using TripFlip.Services.Dto.ItemDtos;
using TripFlip.Services.Interfaces;

namespace WebApiIntegrationTests.ItemServiceTests
{
    public class TestItemServiceBase : TestServiceBase
    {
        protected IEnumerable<int> ValidRouteSubscribersToAssignToItem = new List<int>()
        {
            2
        };

        protected static TripEntity TripEntityToSeed => new TripEntity()
        {
            Id = 1,
            Title = "Trip",
            Description = "Description",
            StartsAt = DateTimeOffset.Parse("28/08/2030 14:00:00",
                CultureInfo.GetCultureInfo("en-GB").DateTimeFormat),
            EndsAt = DateTimeOffset.Parse("30/11/2030 19:00:00",
                CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
        };

        protected RouteEntity RouteEntityToSeed => new RouteEntity()
        {
            Id = 1,
            Title = "Route",
            TripId = 1,
        };

        protected ItemListEntity ItemListEntityToSeed => new ItemListEntity()
        {
            Id = 1,
            Title = "Item list",
            RouteId = 1
        };

        protected ItemEntity ItemEntityToSeed => new ItemEntity()
        {
            Id = 1,
            Title = "Item",
            Comment = "Comment",
            Quantity = "Quantity",
            IsCompleted = false,
            ItemListId = 1,
        };

        protected IEnumerable<TripSubscriberEntity> TripSubscriberEntitiesToSeed =>
            new List<TripSubscriberEntity>()
            {
                new TripSubscriberEntity()
                {
                    Id = 1,
                    TripId = 1,
                    UserId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e")
                },
                new TripSubscriberEntity()
                {
                    Id = 2,
                    TripId = 1,
                    UserId = Guid.Parse("816fe98f-515c-407a-bf66-cc9a908644c1")
                },
                new TripSubscriberEntity()
                {
                    Id = 3,
                    TripId = 1,
                    UserId = Guid.Parse("3ed64e6a-0b5c-423b-a1ec-f0d38c9f6846")
                }
            };

        protected IEnumerable<RouteSubscriberEntity> RouteSubscriberEntitiesToSeed =>
            new List<RouteSubscriberEntity>()
            {
                new RouteSubscriberEntity()
                {
                    Id = 1,
                    RouteId = 1,
                    TripSubscriberId = 1
                },
                new RouteSubscriberEntity()
                {
                    Id = 2,
                    RouteId = 1,
                    TripSubscriberId = 3
                }
            };

        protected IEnumerable<RouteRoleEntity> RouteRoleEntitiesToSeed => new List<RouteRoleEntity>()
        {
            new RouteRoleEntity()
            {
                Id = 1,
                Name = "Admin"
            },
            new RouteRoleEntity()
            {
                Id = 2,
                Name = "Editor"
            }
        };

        protected IEnumerable<RouteSubscriberRoleEntity> RouteSubscriberRoleEntitiesToSeed =>
            new List<RouteSubscriberRoleEntity>()
            {
                new RouteSubscriberRoleEntity()
                {
                    RouteRoleId = 1,
                    RouteSubscriberId = 1
                },
                new RouteSubscriberRoleEntity()
                {
                    RouteRoleId = 2,
                    RouteSubscriberId = 1
                }
            };

        protected static UserEntity ValidUser => new UserEntity()
        {
            Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
            Email = "correct@mail.com"
        };

        protected static UserEntity NonExistentUser => new UserEntity()
        {
            Id = Guid.Parse("322967ec-9415-4778-99c6-7f566d1bb8d2"),
            Email = "nonexistent@mail.com"
        };

        protected static UserEntity NotTripSubscriberUser => new UserEntity()
        {
            Id = Guid.Parse("c44315ef-547e-4366-888a-46d2e057e6f7"),
            Email = "notsuboftrip@mail.com"
        };

        protected static UserEntity NotRouteSubscriberUser => new UserEntity()
        {
            Id = Guid.Parse("816fe98f-515c-407a-bf66-cc9a908644c1"),
            Email = "notsubofroute@mail.com"
        };

        protected static UserEntity RouteSubscriberWithoutRolesUser => new UserEntity()
        {
            Id = Guid.Parse("3ed64e6a-0b5c-423b-a1ec-f0d38c9f6846"),
            Email = "notadminroutrole@mail.com"
        };

        protected RouteSubscriberRoleEntity RouteSubscriberAdminRoleEntityToSeed =>
            new RouteSubscriberRoleEntity()
            {
                RouteRoleId = 1,
                RouteSubscriberId = 1
            };

        protected RouteSubscriberRoleEntity RouteSubscriberEditorRoleEntityToSeed =>
            new RouteSubscriberRoleEntity()
            {
                RouteRoleId = 2,
                RouteSubscriberId = 1
            };

        protected ICurrentUserService CurrentUserService;

        protected static ICurrentUserService CreateCurrentUserService(Guid id, string email)
        {
            var correctEmail = email;
            var correctGuid = id;

            var mock = new Mock<ICurrentUserService>();
            mock.Setup(a => a.UserEmail).Returns(correctEmail);
            mock.Setup(a => a.UserId).Returns(correctGuid);

            return mock.Object;
        }

        protected CreateItemDto GetCreateItemDto(int itemListId = 1, string title = "Title",
            string comment = "Comment", string quantity = "Quantity")
        {
            return new CreateItemDto()
            {
                ItemListId = itemListId,
                Title = title,
                Comment = comment,
                Quantity = quantity
            };
        }

        protected UpdateItemDto GetUpdateItemDto(int itemId = 1, bool isCompleted = true,
            string title = "Updated title", string comment = "Updated comment",
            string quantity = "Updated quantity")
        {
            return new UpdateItemDto()
            {
                Id = itemId,
                IsCompleted = isCompleted,
                Comment = comment,
                Title = title,
                Quantity = quantity
            };
        }

        protected ItemAssigneesDto GetItemAssigneesDto(IEnumerable<int> routeSubscriberIds,
            int taskId = 1)
        {
            return new ItemAssigneesDto()
            {
                RouteSubscriberIds = routeSubscriberIds,
                ItemId = taskId
            };
        }
    }
}