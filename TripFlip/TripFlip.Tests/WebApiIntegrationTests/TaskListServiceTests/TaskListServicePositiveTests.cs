using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.Services;
using TripFlip.Services.Dto.TaskListDtos;
using WebApiIntegrationTests.CustomComparers;

namespace WebApiIntegrationTests.TaskListServiceTests
{
    [TestClass]
    public class TaskListServicePositiveTests : TestTaskListServiceBase
    {
        private readonly TaskListDto _expectedReturnTaskListDto = new TaskListDto()
        {
            Id = 1,
            Title = "Updated title",
            RouteId = 1
        };

        private List<TaskListDto> _expectedGotAllTaskListDtos =
            new List<TaskListDto>()
            {
                new TaskListDto()
                {
                    Id = 1,
                    RouteId = 1,
                    Title = "Task list 1"
                },
                new TaskListDto()
                {
                    Id = 2,
                    RouteId = 1,
                    Title = "Task list 2"
                },
                new TaskListDto()
                {
                    Id = 3,
                    RouteId = 1,
                    Title = "Task list 3"
                }
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
        public async Task GetAllByRouteIdAsync_GivenValidRouteId_Successful()
        {
            // Arrange
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TaskListEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id, ValidUser.Email);

            var taskListService = new TaskListService(TripFlipDbContext,
                Mapper, CurrentUserService);
            var validRouteId = 1;
            var paginationDto = GetPaginationDto();
            string searchString = null;
            var comparer = new TaskListDtoComparer();

            // Act
            var result = await taskListService.GetAllByRouteIdAsync(1,
                searchString, paginationDto);

            var returnedTaskListDtos = result.Items.ToList();

            var expectedTaskListCount = _expectedGotAllTaskListDtos.Count();

            // Assert
            Assert.AreEqual(expectedTaskListCount, result.TotalCount);

            for (var i = 0; i < expectedTaskListCount; i++)
            {
                Assert.AreEqual(0, 
                    comparer.Compare(returnedTaskListDtos[i], _expectedGotAllTaskListDtos[i]));
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ValidData_Successful()
        {
            // Arrange.
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TaskListEntitiesToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEditorRoleEntityToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var updateTaskListDto = GetUpdateTaskListDto();
            var taskListService = new TaskListService(TripFlipDbContext, Mapper, CurrentUserService);

            // Act. 
            var resultTaskListDto =
                await taskListService.UpdateAsync(updateTaskListDto);

            var comparer = new TaskListDtoComparer();

            // Assert.
            Assert.AreEqual(0,
                comparer.Compare(resultTaskListDto, _expectedReturnTaskListDto));
        }
    }
}
