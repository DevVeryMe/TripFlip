using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.ItemViewModels;

namespace WebApiUnitTests.UpdateItemViewModelTests
{
    [TestClass]
    public class UpdateItemViewModelPositiveTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestIdData), DynamicDataSourceType.Method)]
        public void TestItemTitleValidation(string displayName,
            int id)
        {
            // Arrange
            var updateItemViewModel = GetUpdateItemViewModel(id: id);

            // Act
            var result = Validator.TryValidateObject(updateItemViewModel,
                new ValidationContext(updateItemViewModel),
                null,
                true);

            // Assert
            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestTitleData), DynamicDataSourceType.Method)]
        public void TestItemTitleValidation(string displayName,
            string title)
        {
            // Arrange
            var updateItemViewModel = GetUpdateItemViewModel(title: title);

            // Act
            var result = Validator.TryValidateObject(updateItemViewModel,
                new ValidationContext(updateItemViewModel),
                null,
                true);

            // Assert
            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestCommentData), DynamicDataSourceType.Method)]
        public void TestItemCommentValidation(string displayName,
            string comment)
        {
            // Arrange
            var updateItemViewModel = GetUpdateItemViewModel(comment: comment);

            // Act
            var result = Validator.TryValidateObject(updateItemViewModel,
                new ValidationContext(updateItemViewModel),
                null,
                true);

            // Assert
            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestQuantityData), DynamicDataSourceType.Method)]
        public void TestItemQuantityValidation(string displayName,
            string quantity)
        {
            // Arrange
            var updateItemViewModel = GetUpdateItemViewModel(quantity: quantity);

            // Act
            var result = Validator.TryValidateObject(updateItemViewModel,
                new ValidationContext(updateItemViewModel),
                null,
                true);

            // Assert
            Assert.IsTrue(result);
        }

        private static IEnumerable<object[]> GetTestIdData()
        {
            yield return new object[]
            {
                "Test case 1: Create_UpdateItemViewModel_Given_Valid_Id_min_value_" +
                "Validation_should_be_successful",
                1
            };

            yield return new object[]
            {
                "Test case 2: Create_UpdateItemViewModel_Given_Valid_Id_max_value_" +
                "possible_Validation_should_be_successful",
                int.MaxValue
            };

            yield return new object[]
            {
                "Test case 3: Create_UpdateItemViewModel_Given_Valid_Id_average_value_" +
                "Validation_should_be_successful",
                2500
            };
        }

        private static IEnumerable<object[]> GetTestTitleData()
        {
            yield return new object[]
            {
                "Test case 1: Create_UpdateItemViewModel_Given_Valid_Title_min_value_" +
                "Validation_should_be_successful",
                "*"
            };

            yield return new object[]
            {
                "Test case 2: Create_UpdateItemViewModel_Given_Valid_Title_max_value_" +
                "possible_Validation_should_be_successful",
                new string('*', 100)
            };

            yield return new object[]
            {
                "Test case 3: Create_UpdateItemViewModel_Given_Valid_Title_average_value_" +
                "Validation_should_be_successful",
                new string('*', 50)
            };
        }

        private static IEnumerable<object[]> GetTestCommentData()
        {
            yield return new object[]
            {
                "Test case 1: Create_CreateItemViewModel_Given_Valid_Comment_equals_null_" +
                "Validation_should_be_successful",
                null
            };

            yield return new object[]
            {
                "Test case 2: Create_CreateItemViewModel_Given_Valid_Comment_equals_empty_" +
                "Validation_should_be_successful",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3: Create_CreateItemViewModel_Given_Valid_Comment_equals_max_value_" +
                "Validation_should_be_successful",
                new string('*', 250)
            };

            yield return new object[]
            {
                "Test case 4: Create_CreateItemViewModel_Given_Valid_Comment_equals_average_value_" +
                "Validation_should_be_successful",
                new string('*', 125)
            };
        }

        private static IEnumerable<object[]> GetTestQuantityData()
        {
            yield return new object[]
            {
                "Test case 1: Create_CreateItemViewModel_Given_Valid_Quantity_equals_null_" +
                "Validation_should_be_successful",
                null
            };

            yield return new object[]
            {
                "Test case 2: Create_CreateItemViewModel_Given_Valid_Quantity_equals_empty_" +
                "Validation_should_be_successful",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3: Create_CreateItemViewModel_Given_Valid_Quantity_equals_max_value_" +
                "Validation_should_be_successful",
                new string('*', 50)
            };

            yield return new object[]
            {
                "Test case 4: Create_CreateItemViewModel_Given_Valid_Quantity_equals_average_value_" +
                "Validation_should_be_successful",
                new string('*', 25)
            };
        }

        private static UpdateItemViewModel GetUpdateItemViewModel(int id = 1, 
            string title = "Valid value", string comment = null, string quantity = null, 
            bool isCompleted = false)
        {
            return new UpdateItemViewModel()
            {
                Id = id,
                Title = title,
                Comment = comment,
                Quantity = quantity,
                IsCompleted = isCompleted
            };
        }
    }
}
