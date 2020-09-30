using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using TripFlip.Domain.Entities;
using TripFlip.Services.Enums;
using TripFlip.Services.Interfaces;

namespace WebApiIntegrationTests.EntityValidationHelperTests
{
    public abstract class TestEntityValidationHelperBase : TestServiceBase
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
            TripId = 1
        };

        protected IEnumerable<TripSubscriberEntity> TripSubscriberEntitiesToSeed =>
            new List<TripSubscriberEntity>()
            {
                new TripSubscriberEntity()
                {
                    Id = 1,
                    TripId = TripEntityToSeed.Id,
                    UserId = ValidUser.Id
                },
                new TripSubscriberEntity()
                {
                    Id = 2,
                    TripId = TripEntityToSeed.Id,
                    UserId = NotRouteSubscriberUser.Id
                }
            };

        protected IEnumerable<TripSubscriberRoleEntity> TripSubscriberRoleEntitiesToSeed =>
            new List<TripSubscriberRoleEntity>()
            {
                new TripSubscriberRoleEntity()
                {
                    TripRoleId = (int)TripRoles.Admin,
                    TripSubscriberId = 1
                }
            };

        protected RouteSubscriberRoleEntity RouteSubscriberRoleEntityToSeed =>
            new RouteSubscriberRoleEntity()
            {
                RouteRoleId = (int)RouteRoles.Admin,
                RouteSubscriberId = 1
            };

        protected RouteSubscriberEntity RouteSubscriberEntityToSeed =>
            new RouteSubscriberEntity()
            {
                Id = 1,
                RouteId = 1,
                TripSubscriberId = 1
            };

        protected IEnumerable<TripRoleEntity> TripRolesToSeed = new List<TripRoleEntity>()
        {
            new TripRoleEntity()
            {
                Id = (int)TripRoles.Admin,
                Name = TripRoles.Admin.ToString()
            },
            new TripRoleEntity()
            {
                Id = (int)TripRoles.Editor,
                Name = TripRoles.Editor.ToString()
            },
            new TripRoleEntity()
            {
                Id = (int)TripRoles.Guest,
                Name = TripRoles.Guest.ToString()
            }
        };

        protected IEnumerable<RouteRoleEntity> RouteRolesToSeed = new List<RouteRoleEntity>()
        {
            new RouteRoleEntity()
            {
                Id = (int)RouteRoles.Admin,
                Name = RouteRoles.Admin.ToString()
            },
            new RouteRoleEntity()
            {
                Id = (int)RouteRoles.Editor,
                Name = RouteRoles.Editor.ToString()
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
            Id = Guid.Parse("c44315ef-547e-4366-888a-46d2e057e6f8"),
            Email = "notsubofroute@mail.com"
        };

        protected IEnumerable<ApplicationRoleEntity> ApplicationRoleEntitiesToSeed = 
            new List<ApplicationRoleEntity>()
            {
                new ApplicationRoleEntity()
                {
                    Id = 1,
                    Name = "SuperAdmin"
                },
                new ApplicationRoleEntity()
                {
                    Id = 2,
                    Name = "Admin"
                }
            };

        protected ApplicationUserRoleEntity ApplicationUserRoleEntityToSeed =
            new ApplicationUserRoleEntity()
            {
                ApplicationRoleId = 1, 
                UserId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e")
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
    }
}
