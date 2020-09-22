using Moq;
using System;
using System.Collections.Generic;
using TripFlip.Domain.Entities;
using TripFlip.Services.Interfaces;

namespace WebApiIntegrationTests.TripServiceTests
{
    public abstract class TestTripServiceBase : TestServiceBase
    {
        protected static UserEntity UserEntityToSeed = new UserEntity()
        {
            Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
            Email = "string@string.com"
        };

        protected static IEnumerable<TripRoleEntity> TripRolesToSeed = new List<TripRoleEntity>()
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

        protected ICurrentUserService CurrentUserService;

        protected ITripService TripService;

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
