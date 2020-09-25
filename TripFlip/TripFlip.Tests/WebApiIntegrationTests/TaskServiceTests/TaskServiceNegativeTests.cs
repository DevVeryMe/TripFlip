using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var invalidId = 2;

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);
            var taskService = new TaskService(TripFlipDbContext, Mapper,
                CurrentUserService);

            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await taskService.GetByIdAsync(invalidId));
        }

        [TestMethod]
        public async Task DeleteByIdAsync_GivenNotValidId_ExceptionThrown()
        {
            var invalidId = 2;

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);
            var taskService = new TaskService(TripFlipDbContext, Mapper,
                CurrentUserService);

            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await taskService.DeleteByIdAsync(invalidId));
        }
    }
}
