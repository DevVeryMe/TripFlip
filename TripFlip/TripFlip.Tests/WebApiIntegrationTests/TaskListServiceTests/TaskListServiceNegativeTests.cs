using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TripFlip.Services;
using TripFlip.Services.CustomExceptions;

namespace WebApiIntegrationTests.TaskListServiceTests
{
    [TestClass]
    public class TaskListServiceNegativeTests : TestTaskListServiceBase
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
        public async Task GetAllByRouteIdAsync_GivenInvalidRouteId_ExceptionThrown()
        {
            // Arrange
            Seed(TripFlipDbContext, TaskListEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id, ValidUser.Email);

            var taskListService = new TaskListService(TripFlipDbContext, 
                Mapper, CurrentUserService);
            var invalidRouteId = 2;
            var paginationDto = GetPaginationDto();
            string searchString = null;

            // Act + Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await taskListService.GetAllByRouteIdAsync(invalidRouteId, 
                    searchString, paginationDto));
        }
    }
}
