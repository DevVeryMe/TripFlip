using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TripFlip.Services;
using TripFlip.Services.Dto.Enums;
using TripFlip.Services.Dto.TaskDtos;
using WebApiIntegrationTests.CustomComparers;

namespace WebApiIntegrationTests.TaskServiceTests
{
    [TestClass]
    public class TaskServicePositiveTests : TestTaskServiceBase
    {
        private readonly TaskDto _expectedGotByIdTaskDto = new TaskDto()
        {
            Id = 1,
            Description = "Task",
            IsCompleted = false,
            PriorityLevel = TaskPriorityLevel.Low,
            TaskListId = 1
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
        public async Task GetById_GivenValidId_Successful()
        {
            // Arrange.
            Seed(TripFlipDbContext, TaskEntityToSeed);

            var validTaskId = 1;

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var taskService = new TaskService(TripFlipDbContext, Mapper,
                CurrentUserService);
            var comparer = new TaskDtoComparer();

            // Act.
            var resultTaskDto = await taskService.GetByIdAsync(validTaskId);
            
            // Assert.
            Assert.AreEqual(0, comparer.Compare(_expectedGotByIdTaskDto, resultTaskDto));
        }

        [TestMethod]
        public async Task CreateAsync_ValidData_Successful()
        {
            var taskListEntityToSeed = TaskListEntityToSeed;

            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, taskListEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberAdminRoleEntityToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var createTaskDto = GetCreateTaskDto();
            var taskService = new TaskService(TripFlipDbContext, Mapper,
                CurrentUserService);

            var expectedTaskDto = new TaskDto()
            {
                Id = 1,
                IsCompleted = false,
                Description = createTaskDto.Description,
                PriorityLevel = createTaskDto.PriorityLevel,
                TaskListId = createTaskDto.TaskListId
            };

            var resultTaskDto = await taskService.CreateAsync(createTaskDto);

            var taskDtoComparer = new TaskDtoComparer();

            Assert.AreEqual(0,
                taskDtoComparer.Compare(expectedTaskDto, resultTaskDto));
        }
    }
}
