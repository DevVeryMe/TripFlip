using System;
using Moq;
using TripFlip.Services.Interfaces;

namespace WepApiIntegrationTests.TripServiceTests
{
    public class TestTripServiceBase : TestServiceBase
    {
        private readonly string _correctEmail = "string@string.com";

        private readonly Guid _correctGuid = Guid.NewGuid();

        protected ICurrentUserService CurrentUserService;

        public TestTripServiceBase()
        {
            CurrentUserService = CreateCurrentUserServiceWithCorrectData();
        }

        private ICurrentUserService CreateCurrentUserServiceWithCorrectData()
        {
            var mock = new Mock<ICurrentUserService>();
            mock.Setup(a => a.UserEmail).Returns(_correctEmail);
            mock.Setup(a => a.UserId).Returns(_correctGuid);

            return mock.Object;
        }
    }
}
