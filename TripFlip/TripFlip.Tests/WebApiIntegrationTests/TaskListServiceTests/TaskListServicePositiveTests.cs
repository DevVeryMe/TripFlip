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
        public async Task GetByIdAsync_ExistentTaskListId_Successful()
        {
            // Arrange.
            var taskListEntityToSeed = TaskListEntityToSeed;

            Seed(TripFlipDbContext, taskListEntityToSeed);

            var existentTaskListId = taskListEntityToSeed.Id;
            var taskListService = new TaskListService(TripFlipDbContext, Mapper,
                CurrentUserService);

            var expectedTaskListDto = Mapper.Map<TaskListDto>(taskListEntityToSeed);
            var taskListDtoComparer = new TaskListDtoComparer();

            // Act.
            var resultTaskListDto = await taskListService.GetByIdAsync(existentTaskListId);

            // Act + Assert.
            Assert.AreEqual(0,
                taskListDtoComparer.Compare(expectedTaskListDto, resultTaskListDto));
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

        [TestMethod]
        public async Task DeleteById_ExistentTaskListId_Successful()
        {
            // Arrange.
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TaskListEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberAdminRoleEntityToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var existentTaskListId = 1;
            var taskListService = new TaskListService(TripFlipDbContext, Mapper,
                CurrentUserService);

            // Act.
            await taskListService.DeleteByIdAsync(existentTaskListId);

            var taskListEntity = await TripFlipDbContext
                .TaskLists
                .FindAsync(existentTaskListId);

            // Assert.
            Assert.IsNull(taskListEntity);
        }

        [TestMethod]
        public async Task CreateAsync_ValidData_Successful()
        {
            // Arrange.
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberAdminRoleEntityToSeed);

            var comparer = new TaskListDtoComparer();

            CurrentUserService = CreateCurrentUserService(ValidUser.Id, ValidUser.Email);

            var taskListService = new TaskListService(TripFlipDbContext, Mapper,
                CurrentUserService);

            var taskListEntity = TaskListEntityToSeed;
            var expectedTaskListDto = Mapper.Map<TaskListDto>(taskListEntity);

            var createTaskListDto = Get_CreateTaskListDto(
                routeId: taskListEntity.Id,
                title: taskListEntity.Title);

            // Act.
            var resultTaskListDto = await taskListService.CreateAsync(createTaskListDto);

            // Arrange.
            Assert.AreEqual(0, comparer.Compare(expectedTaskListDto, resultTaskListDto));
        }
    }
}
