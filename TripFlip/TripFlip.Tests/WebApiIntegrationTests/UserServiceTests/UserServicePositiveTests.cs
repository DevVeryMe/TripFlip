using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TripFlip.Services;
using TripFlip.Services.Dto.UserDtos;
using WebApiIntegrationTests.CustomComparers;

namespace WebApiIntegrationTests.UserServiceTests
{
    [TestClass]
    public class UserServicePositiveTests : TestUserServiceBase
    {
        private readonly UserDto _expectedRegisteredUser = new UserDto()
        {
            Email = "correct@mail.com"
        };

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
        public async Task RegisterAsync_GivenValidUserData_Successful()
        {
            // Arrange
            var jwtConfiguration = CreateJwtConfiguration();
            var userService = new UserService(Mapper, TripFlipDbContext,
                jwtConfiguration, CurrentUserService);
            var registerUserDto = GetRegisterUserDto();
            var comparer = new UserDtoComparer();

            // Act
            var result = await userService.RegisterAsync(registerUserDto);

            // Assert
            Assert.AreEqual(0, comparer.Compare(_expectedRegisteredUser, result));
        }
    }
}
