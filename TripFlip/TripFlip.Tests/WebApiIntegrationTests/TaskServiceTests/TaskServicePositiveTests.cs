﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TaskListEntityToSeed);
            Seed(TripFlipDbContext, TaskEntityToSeed);

            var validTaskId = 1;

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var taskService = new TaskService(TripFlipDbContext, Mapper,
                CurrentUserService);

            var resultTaskDto = await taskService.GetByIdAsync(validTaskId);
            var compaper = new TaskDtoComparer();

            Assert.AreEqual(0, compaper.Compare(_expectedGotByIdTaskDto, resultTaskDto));
        }
    }
}
