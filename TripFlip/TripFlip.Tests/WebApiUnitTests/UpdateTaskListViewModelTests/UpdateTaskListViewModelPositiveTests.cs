using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.TaskListViewModels;

namespace WebApiUnitTests.UpdateTaskListViewModelTests
{
    [TestClass]
    public class UpdateTaskListViewModelPositiveTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestIdData), DynamicDataSourceType.Method)]
        public void TestTaskListIdValidation(string displayName,
            int id)
        {
            // Arrange
            var updateTaskListViewModel = GetUpdateTaskListViewModel(id: id);

            // Act
            var result = Validator.TryValidateObject(updateTaskListViewModel,
                new ValidationContext(updateTaskListViewModel),
                null,
                true);

            // Assert
            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestTitleData), DynamicDataSourceType.Method)]
        public void TestTaskListTitleValidation(string displayName,
            string title)
        {
            // Arrange
            var updateTaskListViewModel = GetUpdateTaskListViewModel(title: title);

            // Act
            var result = Validator.TryValidateObject(updateTaskListViewModel,
                new ValidationContext(updateTaskListViewModel),
                null,
                true);

            // Assert
            Assert.IsTrue(result);
        }

        private static IEnumerable<object[]> GetTestIdData()
        {
            yield return new object[]
            {
                "Test case 1: Create_UpdateTaskListViewModel_Given_Valid_Id_equals_min_value_" +
                "Validation_should_be_successful",
                1
            };

            yield return new object[]
            {
                "Test case 2: Create_UpdateTaskListViewModel_Given_Valid_Id_equals_average_value_" +
                "Validation_should_be_successful",
                2500
            };

            yield return new object[]
            {
                "Test case 3: Create_UpdateTaskListViewModel_Given_Valid_Id_equals_max_value_" +
                "Validation_should_be_successful",
                int.MaxValue
            };
        }

        private static IEnumerable<object[]> GetTestTitleData()
        {
            yield return new object[]
            {
                "Test case 1: Create_UpdateTaskListViewModel_Given_Valid_Title_equals_min_value_" +
                "Validation_should_be_successful",
                "*"
            };

            yield return new object[]
            {
                "Test case 2: Create_UpdateTaskListViewModel_Given_Valid_Title_equals_max_value_" +
                "Validation_should_be_successful",
                new string('*', 100)
            };

            yield return new object[]
            {
                "Test case 3: Create_UpdateTaskListViewModel_Given_Valid_Title_equals_average_value_" +
                "Validation_should_be_successful",
                new string('*', 50)
            };
        }

        private UpdateTaskListViewModel GetUpdateTaskListViewModel(
            int id = 1, string title = "Valid value")
        {
            return new UpdateTaskListViewModel()
            {
                Id = id,
                Title = title
            };
        }
    }
}
