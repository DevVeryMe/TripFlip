using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using TripFlip.Domain.Entities;
using TripFlip.Services.Interfaces;

namespace WebApiIntegrationTests.TaskListServiceTests
{
    public class TestTaskListServiceBase : TestServiceBase
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

        protected IEnumerable<TaskListEntity> TaskListEntitiesToSeed =>
            new List<TaskListEntity>()
            {
                new TaskListEntity()
                {
                    Id = 1,
                    RouteId = 1,
                    Title = "Task list 1"
                },
                new TaskListEntity()
                {
                    Id = 2,
                    RouteId = 1,
                    Title = "Task list 2"
                },
                new TaskListEntity()
                {
                    Id = 3,
                    RouteId = 1,
                    Title = "Task list 3"
                }
            };

        protected static UserEntity ValidUser => new UserEntity()
        {
            Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
            Email = "correct@mail.com"
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
