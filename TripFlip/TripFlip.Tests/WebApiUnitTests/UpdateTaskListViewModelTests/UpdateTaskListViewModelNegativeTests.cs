using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.TaskListViewModels;

namespace WebApiUnitTests.UpdateTaskListViewModelTests
{
    [TestClass]
    public class UpdateTaskListViewModelNegativeTests
    {
        [TestMethod]
        public void Create_UpdateTaskViewModel_Given_Not_valid_Id_Validation_should_be_failed()
        {
            // Arrange
            var createTaskListViewModel = GetUpdateTaskListViewModel(id: -1);

            // Act
            var result = Validator.TryValidateObject(createTaskListViewModel,
                new ValidationContext(createTaskListViewModel),
                null,
                true);

            // Assert
            Assert.IsFalse(result);
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
            Assert.IsFalse(result);
        }

        private static IEnumerable<object[]> GetTestTitleData()
        {
            yield return new object[]
            {
                "Test case 1: Create_UpdateTaskListViewModel_Given_Not_valid_Title_equals_null_" +
                "Validation_should_be_failed",
                null
            };

            yield return new object[]
            {
                "Test case 2: Create_UpdateTaskListViewModel_Given_Not_valid_Title_equals_empty_string_" +
                "Validation_should_be_failed",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3: Create_UpdateTaskListViewModel_Given_Not_valid_Title_too_long_length_" +
                "Validation_should_be_failed",
                new string('*', 101)
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
