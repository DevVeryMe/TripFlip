using Moq;
using System;
using System.Collections.Generic;
using TripFlip.Domain.Entities;
using TripFlip.Services.Dto.TripDtos;
using TripFlip.Services.Interfaces;

namespace WebApiIntegrationTests.TripServiceTests
{
    public abstract class TestTripServiceBase : TestServiceBase
    {
        protected UserEntity UserEntityToSeed = new UserEntity()
        {
            Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
            Email = "string@string.com"
        };

        protected TripEntity TripEntityToSeed = new TripEntity()
        {
            Id = 1,
            Title = "Title",
            Description = "Description",
            StartsAt = new DateTimeOffset(DateTime.Now.AddDays(5)),
            EndsAt = new DateTimeOffset(DateTime.Now.AddDays(10)),
            DateCreated = new DateTimeOffset(DateTime.Now)
        };

        protected TripSubscriberEntity TripSubscriberEntityToSeed =
            new TripSubscriberEntity()
            {
                Id = 1,
                UserId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                TripId = 1,
                DateSubscribed = new DateTimeOffset(DateTime.Now)
            };

        protected TripSubscriberRoleEntity TripSubscriberAdminRoleEntityToSeed =
            new TripSubscriberRoleEntity()
            {
                TripRoleId = 1,
                TripSubscriberId = 1
            };

        protected IEnumerable<TripRoleEntity> TripRolesToSeed = new List<TripRoleEntity>()
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

        protected static ICurrentUserService CreateCurrentUserServiceWithExistentUser()
        {
            var correctEmail = "string@string.com";
            var correctGuid = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e");
            
            var mock = new Mock<ICurrentUserService>();
            mock.Setup(a => a.UserEmail).Returns(correctEmail);
            mock.Setup(a => a.UserId).Returns(correctGuid);

            return mock.Object;
        }

        protected static ICurrentUserService CreateCurrentUserServiceWithNonExistentUser()
        {
            var correctEmail = "string@mail.com";
            var correctGuid = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7");

            var mock = new Mock<ICurrentUserService>();
            mock.Setup(a => a.UserEmail).Returns(correctEmail);
            mock.Setup(a => a.UserId).Returns(correctGuid);

            return mock.Object;
        }

        protected static UpdateTripDto GetUpdateTripDto(int id = 1)
        {
            return new UpdateTripDto()
            {
                Id = id,
                Title = "New Title",
                Description = "New Description",
                StartsAt = new DateTimeOffset(DateTime.Now.AddDays(10)),
                EndsAt = new DateTimeOffset(DateTime.Now.AddDays(15))
            };
        }
    }
}
