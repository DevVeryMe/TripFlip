using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using TripFlip.Services;

namespace WebApiIntegrationTests.UserServiceTests
{
    [TestClass]
    public class UserServiceNegativeTests : TestUserServiceBase
    {
        [TestInitialize]
        public void Initialize()
        {
            TripFlipDbContext = CreateDbContext();
        }

        [TestCleanup]
        public void Cleanup()
        {
            TripFlipDbContext.Dispose();
        }

        [TestMethod]
        public async Task RegisterAsync_GivenExistentUser_ExceptionThrown()
        {
            // Arrange
            Seed(TripFlipDbContext, ValidUser);
            var jwtConfiguration = CreateJwtConfiguration();
            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);
            var registerUserDto = GetRegisterUserDto(email: ValidUser.Email);

            // Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => 
                await userService.RegisterAsync(registerUserDto));
        }
    }
}
