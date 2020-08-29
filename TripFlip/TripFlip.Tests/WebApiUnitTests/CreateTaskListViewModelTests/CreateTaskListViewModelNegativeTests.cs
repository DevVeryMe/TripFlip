using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.TaskListViewModels;

namespace WebApiUnitTests.CreateTaskListViewModelTests
{
    [TestClass]
    public class CreateTaskListViewModelNegativeTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestTitleData), DynamicDataSourceType.Method)]
        public void TestTaskListTitleValidation(string displayName, string title)
        {
            // Arrange
            var createTaskListViewModel = GetCreateTaskListViewModel(title: title);

            // Act
            var result = Validator.TryValidateObject(createTaskListViewModel,
                new ValidationContext(createTaskListViewModel),
                null,
                true);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Create_CreateTaskViewModel_Given_Not_valid_RouteId_Validation_should_be_failed()
        {
            // Arrange
            var createTaskListViewModel = GetCreateTaskListViewModel(routeId: -1);

            // Act
            var result = Validator.TryValidateObject(createTaskListViewModel,
                new ValidationContext(createTaskListViewModel),
                null,
                true);

            // Assert
            Assert.IsFalse(result);
        }

        private static IEnumerable<object[]> GetTestTitleData()
        {
            yield return new object[]
            {
                "Test case 1: Create_CreateTaskListViewModel_Given_Not_valid_Title_equals_null_" +
                "Validation_should_be_failed",
                null
            };

            yield return new object[]
            {
                "Test case 2: Create_CreateTaskListViewModel_Given_Not_valid_Title_equals_empty_string_" +
                "Validation_should_be_failed",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3: Create_CreateTaskListViewModel_Given_Not_valid_Title_too_long_length_" +
                "Validation_should_be_failed",
                new string('*', 101)
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
