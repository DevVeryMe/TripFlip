using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TripFlip.Services;
using TripFlip.Services.CustomExceptions;

namespace WebApiIntegrationTests.TaskServiceTests
{
    [TestClass]
    public class TaskServiceNegativeTests : TestTaskServiceBase
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
        public async Task GetByIdAsync_GivenNotValidId_ExceptionThrown()
        {
            // Arrange
            var invalidId = 2;

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);
            var taskService = new TaskService(TripFlipDbContext, Mapper,
                CurrentUserService);

            // Act + Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await taskService.GetByIdAsync(invalidId));
        }
    }
}
