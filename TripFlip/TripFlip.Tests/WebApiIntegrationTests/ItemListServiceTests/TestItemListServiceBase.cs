using Moq;
using System;
using System.Globalization;
using TripFlip.Domain.Entities;
using TripFlip.Services.Interfaces;

namespace WebApiIntegrationTests.ItemListServiceTests
{
    public class TestItemListServiceBase : TestServiceBase
    {
        protected static UserEntity[] UserEntitiesToSeed =
        {
            new UserEntity()
            {
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                Email = "string@string.com"
            },
            new UserEntity()
            {
                Id = Guid.Parse("322967ec-9415-4778-99c6-7f566d1bb8d2"),
                Email = "string@mail.com"
            },
            new UserEntity()
            {
                Id = Guid.Parse("c44315ef-547e-4366-888a-46d2e057e6f7"),
                Email = "mail@string.com"
            },
    };
        

        protected static TripEntity TripEntityToSeed = new TripEntity()
        {
            Id = 1,
            Title = "Trip",
            Description = "Description",
            StartsAt = DateTimeOffset.Parse("28/08/2030 14:00:00",
                CultureInfo.GetCultureInfo("en-GB").DateTimeFormat),
            EndsAt = DateTimeOffset.Parse("30/11/2030 19:00:00",
                CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
        };

        protected static RouteEntity RouteEntityToSeed = new RouteEntity()
        {
            Id = 1,
            Title = "Route",
            TripId = 1,

        };

        protected static TripSubscriberEntity[] TripSubscriberEntitiesToSeed =
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
                UserId = Guid.Parse("322967ec-9415-4778-99c6-7f566d1bb8d2")
            },
        };   

        protected static RouteSubscriberEntity RouteSubscriberEntityToSeed = new RouteSubscriberEntity()
        {
            Id = 1,
            RouteId = 1,
            TripSubscriberId = 1
        };

        protected static RouteRoleEntity RouteRoleEntityToSeed = new RouteRoleEntity()
        {
            Id = 1,
            Name = "Admin"
        };

        protected static RouteSubscriberRoleEntity RouteSubscriberRoleEntityToSeed = 
            new RouteSubscriberRoleEntity()
            {
                RouteRoleId = 1,
                RouteSubscriberId = 1
            };

        protected ICurrentUserService CreateCurrentUserServiceWithExistentUser()
        {
            var correctEmail = "string@string.com";
            var correctGuid = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e");

            var mock = new Mock<ICurrentUserService>();
            mock.Setup(a => a.UserEmail).Returns(correctEmail);
            mock.Setup(a => a.UserId).Returns(correctGuid);

            return mock.Object;
        }

        protected ICurrentUserService CreateCurrentUserServiceWithNonExistentUser()
        {
            var correctEmail = "string@mail.com";
            var correctGuid = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7");

            var mock = new Mock<ICurrentUserService>();
            mock.Setup(a => a.UserEmail).Returns(correctEmail);
            mock.Setup(a => a.UserId).Returns(correctGuid);

            return mock.Object;
        }
    }
}
