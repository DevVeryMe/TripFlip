﻿using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using TripFlip.Domain.Entities;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.ItemListDtos;
using TripFlip.Services.Interfaces;

namespace WebApiIntegrationTests.ItemListServiceTests
{
    public class TestItemListServiceBase : TestServiceBase
    {
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
            Title = "ItemList",
            RouteId = 1
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

        protected IEnumerable<RouteRoleEntity> RouteRoleEntitiesToSeed =>
            new List<RouteRoleEntity>()
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

        protected static UserEntity UserWithoutRouteRoles => new UserEntity()
        {
            Id = Guid.Parse("3ed64e6a-0b5c-423b-a1ec-f0d38c9f6846"),
            Email = "notadminroutrole@mail.com"
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

        protected UpdateItemListDto GetUpdateItemListDto(int id = 1,
            string title = "Title")
        {
            return new UpdateItemListDto()
            {
                Id = id,
                Title = title
            };
        }

        protected CreateItemListDto GetCreateItemListDto(int routeId = 1,
            string title = "Title")
        {
            return new CreateItemListDto()
            {
                RouteId = routeId,
                Title = title
            };
        }

        protected PaginationDto GetPaginationDto(int? pageNumber = null,
            int? pageSize = null)
        {
            return new PaginationDto()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
