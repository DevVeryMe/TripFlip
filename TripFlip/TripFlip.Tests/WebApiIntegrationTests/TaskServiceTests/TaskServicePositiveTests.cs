using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
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
            // Arrange
            Seed(TripFlipDbContext, TaskEntityToSeed);
            
            var validTaskId = 1;

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var taskService = new TaskService(TripFlipDbContext, Mapper,
                CurrentUserService);
            var comparer = new TaskDtoComparer();

            // Act
            var resultTaskDto = await taskService.GetByIdAsync(validTaskId);
            
            // Assert
            Assert.AreEqual(0, comparer.Compare(_expectedGotByIdTaskDto, resultTaskDto));
        }

        [TestMethod]
        public async Task DeleteById_GivenValidId_Successful()
        {
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TaskListEntityToSeed);
            Seed(TripFlipDbContext, TaskEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntityToSeed);
            Seed(TripFlipDbContext, RouteSubscriberRoleEntitiesToSeed);
            Seed(TripFlipDbContext, ValidUser);

            var validTaskId = 1;
            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var taskService = new TaskService(TripFlipDbContext, Mapper,
                CurrentUserService);

            await taskService.DeleteByIdAsync(validTaskId);

            var taskEntity = await TripFlipDbContext.Tasks.FindAsync(validTaskId);

            Assert.IsNull(taskEntity);
        }
    }
}
