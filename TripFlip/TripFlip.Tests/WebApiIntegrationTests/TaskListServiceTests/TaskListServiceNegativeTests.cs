using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TripFlip.Services;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Dto;

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
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TaskListEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id, ValidUser.Email);

            var taskListService = new TaskListService(TripFlipDbContext, 
                Mapper, CurrentUserService);
            var invalidRouteId = 2;
            var paginationDto = GetPaginationDto();
            string searchString = null;

            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await taskListService.GetAllByRouteIdAsync(invalidRouteId, 
                    searchString, paginationDto));
        }

        private PaginationDto GetPaginationDto(int? pageNumber = null,
            int? pageSize = null)
        {
            return new PaginationDto()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
