using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.TaskListViewModels;

namespace WebApiUnitTests.CreateTaskListViewModelTests
{
    [TestClass]
    public class CreateTaskViewModelPositiveTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestTitleData), DynamicDataSourceType.Method)]
        public void TestTaskListTitleValidation(string displayName,
            string title)
        {
            // Arrange
            var createTaskViewViewModel = GetCreateTaskListViewModel(title: title);

            // Act
            var result = Validator.TryValidateObject(createTaskViewViewModel,
                new ValidationContext(createTaskViewViewModel),
                null,
                true);

            // Assert
            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestRouteIdData), DynamicDataSourceType.Method)]
        public void TestTaskListRouteIdValidation(string displayName,
            int routeId)
        {
            // Arrange
            var createTaskViewViewModel = GetCreateTaskListViewModel(routeId: routeId);

            // Act
            var result = Validator.TryValidateObject(createTaskViewViewModel,
                new ValidationContext(createTaskViewViewModel),
                null,
                true);

            // Assert
            Assert.IsTrue(result);
        }

        private static IEnumerable<object[]> GetTestTitleData()
        {
            yield return new object[]
            {
                "Test case 1: Create_CreateTaskListViewModel_Given_Valid_Title_equals_min_value_" +
                "Validation_should_be_successful",
                "*"
            };

            yield return new object[]
            {
                "Test case 2: Create_CreateTaskListViewModel_Given_Valid_Title_equals_max_value_" +
                "Validation_should_be_successful",
                new string('*', 100)
            };

            yield return new object[]
            {
                "Test case 3: Create_CreateTaskListViewModel_Given_Valid_Title_equals_average_value_" +
                "Validation_should_be_successful",
                new string('*', 50)
            };
        }

        private static IEnumerable<object[]> GetTestRouteIdData()
        {
            yield return new object[]
            {
                "Test case 1: Create_CreateTaskListViewModel_Given_Valid_RouteId_equals_min_value_" +
                "Validation_should_be_successful",
                1
            };

            yield return new object[]
            {
                "Test case 2: Create_CreateTaskListViewModel_Given_Valid_RouteId_equals_average_value_" +
                "Validation_should_be_successful",
                2500
            };

            yield return new object[]
            {
                "Test case 3: Create_CreateTaskListViewModel_Given_Valid_RouteId_equals_max_value_" +
                "Validation_should_be_successful",
                int.MaxValue
            };
        }

        private CreateTaskListViewModel GetCreateTaskListViewModel(
            string title = "Valid value", int routeId = 1)
        {
            return new CreateTaskListViewModel()
            {
                Title = title,
                RouteId = routeId
            };
        }
    }
}
