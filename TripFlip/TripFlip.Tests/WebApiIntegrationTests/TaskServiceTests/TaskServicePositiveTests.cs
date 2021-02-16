using Microsoft.EntityFrameworkCore;
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
            // Arrange.
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

            // Act.
            var resultTaskDto = await taskService.CreateAsync(createTaskDto);

            var taskDtoComparer = new TaskDtoComparer();

            // Assert.
            Assert.AreEqual(0,
                taskDtoComparer.Compare(expectedTaskDto, resultTaskDto));
        }

        [TestMethod]
        public async Task DeleteById_GivenValidId_Successful()
        {
            // Arrange
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TaskListEntityToSeed);
            Seed(TripFlipDbContext, TaskEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberAdminRoleEntityToSeed);
            Seed(TripFlipDbContext, ValidUser);

            var validTaskId = 1;
            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var taskService = new TaskService(TripFlipDbContext, Mapper,
                CurrentUserService);

            // Act
            await taskService.DeleteByIdAsync(validTaskId);

            // Assert
            var taskEntity = await TripFlipDbContext.Tasks.FindAsync(validTaskId);

            Assert.IsNull(taskEntity);
        }

        [TestMethod]
        public async Task UpdatePriorityAsync_ValidData_Successful()
        {
            // Arrange.
            var taskEntityToSeed = TaskEntityToSeed;

            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TaskListEntityToSeed);
            Seed(TripFlipDbContext, taskEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEditorRoleEntityToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var updateTaskPriorityDto = GetUpdateTaskPriorityDto();
            var taskService = new TaskService(TripFlipDbContext, Mapper,
                CurrentUserService);

            var expectedTaskDto = new TaskDto()
            {
                Id = updateTaskPriorityDto.Id,
                PriorityLevel = updateTaskPriorityDto.PriorityLevel,
                Description = taskEntityToSeed.Description,
                IsCompleted = taskEntityToSeed.IsCompleted,
                TaskListId = taskEntityToSeed.TaskListId
            };

            var taskDtoComparer = new TaskDtoComparer();

            // Act.
            var resultTaskDto =
                await taskService.UpdatePriorityAsync(updateTaskPriorityDto);

            // Assert.
            Assert.AreEqual(0,
                taskDtoComparer.Compare(expectedTaskDto, resultTaskDto));
        }

        [TestMethod]
        public async Task SetTaskAssigneesAsync_ValidData_Successful()
        {
            // Arrange.
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TaskListEntityToSeed);
            Seed(TripFlipDbContext, TaskEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEditorRoleEntityToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var taskAssigneesDto = GetTaskAssigneesDto(routeSubscriberIds:
                ValidRouteSubscribersToAssignToTask);
            var taskService = new TaskService(TripFlipDbContext, Mapper,
                CurrentUserService);

            // Act.
            await taskService.SetTaskAssigneesAsync(taskAssigneesDto);

            // Assert.

            var task = TripFlipDbContext.Tasks
                .Include(task => task.TaskAssignees)
                .FirstOrDefault(task => task.Id == TaskEntityToSeed.Id);
            Assert.IsNotNull(task);
            Assert.AreEqual(1, task.TaskAssignees.Count);
        }

        [TestMethod]
        public async Task GetAllByTaskListIdAsync_ValidTaskListId_Successful()
        {
            // Arrange
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TaskListEntityToSeed);
            Seed(TripFlipDbContext, TaskEntityToSeed);

            int validTaskListId = 1;

            var taskService = new TaskService(
                tripFlipDbContext: TripFlipDbContext,
                mapper: Mapper,
                currentUserService: null);

            var comparer = new TaskDtoComparer();

            var expectedPagedTaskDto = ExpectedPagedTaskDto;
            var expectedTaskDtoList = expectedPagedTaskDto.Items.ToList();

            // Act
            var resultPagedTaskDto = await taskService.GetAllByTaskListIdAsync(
                taskListId: validTaskListId,
                searchString: null,
                paginationDto: PaginationDto);

            // Assert
            var resultTaskDtoList = resultPagedTaskDto.Items.ToList();
            int resultTaskDtoListCount = resultTaskDtoList.Count;

            Assert.AreEqual(resultTaskDtoListCount, expectedTaskDtoList.Count);

            for (int i = 0; i < resultTaskDtoListCount; i++)
            {
                Assert.AreEqual(0,
                    comparer.Compare(resultTaskDtoList[i], expectedTaskDtoList[i]));
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ValidUserAndTaskDto_Successful()
        {
            // Arrange
            var validUserThatIsRouteEditor = ValidUserWithRouteEditorRole;
            Seed(TripFlipDbContext, validUserThatIsRouteEditor);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TaskListEntityToSeed);
            Seed(TripFlipDbContext, TaskEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(
                id: validUserThatIsRouteEditor.Id, email: validUserThatIsRouteEditor.Email);

            var taskService = new TaskService(
                tripFlipDbContext: TripFlipDbContext,
                mapper: Mapper,
                currentUserService: CurrentUserService);

            var validUpdateTaskDto = UpdateTaskDto;

            var comparer = new TaskDtoComparer();
            var expectedTaskDto = TaskDto;

            // Act
            var resultTaskDto = await taskService.UpdateAsync(validUpdateTaskDto);

            // Assert
            Assert.AreEqual(0, comparer.Compare(resultTaskDto, expectedTaskDto));
        }

        [TestMethod]
        public async Task UpdateCompletenessAsync_ValidUserAndTaskDto_Successful()
        {
            // Arrange
            var validUserThatIsRouteEditor = ValidUserWithRouteEditorRole;
            Seed(TripFlipDbContext, validUserThatIsRouteEditor);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TaskListEntityToSeed);
            Seed(TripFlipDbContext, TaskEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(
                id: validUserThatIsRouteEditor.Id, email: validUserThatIsRouteEditor.Email);

            var taskService = new TaskService(
                tripFlipDbContext: TripFlipDbContext,
                mapper: Mapper,
                currentUserService: CurrentUserService);

            var validUpdateTaskCompletenessDto = UpdateTaskCompletenessDto;

            var comparer = new TaskDtoComparer();

            var expectedTaskDto = TaskDto;
            expectedTaskDto.IsCompleted = true;

            // Act
            var resultTaskDto = await taskService
                .UpdateCompletenessAsync(validUpdateTaskCompletenessDto);

            // Assert
            Assert.AreEqual(0, comparer.Compare(resultTaskDto, expectedTaskDto));
        }
    }
}
