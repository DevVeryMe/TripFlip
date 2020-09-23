using Moq;
using System;
using System.Globalization;
using TripFlip.Domain.Entities;
using TripFlip.Services.Interfaces;

namespace WebApiIntegrationTests.ItemListServiceTests
{
    public class TestItemServiceBase : TestServiceBase
    {
        protected static UserEntity UserEntityToSeed = new UserEntity()
        {
            Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
            Email = "string@string.com"
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
