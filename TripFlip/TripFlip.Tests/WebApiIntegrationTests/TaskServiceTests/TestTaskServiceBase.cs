using Moq;
using System;
using System.Globalization;
using TripFlip.Domain.Entities;
using TripFlip.Domain.Entities.Enums;
using TripFlip.Services.Interfaces;

namespace WebApiIntegrationTests.TaskServiceTests
{
    public class TestTaskServiceBase : TestServiceBase
    {
        protected static UserEntity ValidUser => new UserEntity()
        {
            Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
            Email = "correct@mail.com"
        };

        protected TaskEntity TaskEntityToSeed => new TaskEntity()
        {
            Id = 1,
            Description = "Task",
            IsCompleted = false,
            PriorityLevel = TaskPriorityLevel.Low,
            TaskListId = 1
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
