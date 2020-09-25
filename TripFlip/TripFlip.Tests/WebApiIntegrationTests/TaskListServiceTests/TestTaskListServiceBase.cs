using Moq;
using System;
using TripFlip.Services.Interfaces;

namespace WebApiIntegrationTests.TaskListServiceTests
{
    public class TestTaskListServiceBase : TestServiceBase
    {
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
