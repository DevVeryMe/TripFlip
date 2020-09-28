using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using TripFlip.Domain.Entities;
using TripFlip.Services.Dto.RouteDtos;
using TripFlip.Services.Interfaces;

namespace WebApiIntegrationTests.RouteServiceTests
{
    public class TestRouteServiceBase : TestServiceBase
    {
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

        protected IEnumerable<TripRoleEntity> TripRoleEntitiesToSeed =>
            new List<TripRoleEntity>()
            {
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
                }
            };

        protected IEnumerable<TripSubscriberRoleEntity> TripSubscriberRoleEntitiesToSeed =>
            new List<TripSubscriberRoleEntity>()
            {
                new TripSubscriberRoleEntity()
                {
                    TripRoleId = 1,
                    TripSubscriberId = 1
                },
                new TripSubscriberRoleEntity()
                {
                    TripRoleId = 2,
                    TripSubscriberId = 1
                }
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

        protected CreateRouteDto GetCreateRouteDto(string title = "Route",
            int tripId = 1)
        {
            return new CreateRouteDto()
            {
                Title = title,
                TripId = tripId
            };
        }
    }
}
