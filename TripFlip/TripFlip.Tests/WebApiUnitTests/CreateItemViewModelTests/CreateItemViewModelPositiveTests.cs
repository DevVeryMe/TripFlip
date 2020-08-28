using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.ItemViewModels;

namespace WebApiUnitTests.CreateItemViewModelTests
{
    [TestClass]
    public class CreateItemViewModelPositiveTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestTitleData), DynamicDataSourceType.Method)]
        public void TestItemTitleValidation(string displayName,
            string title)
        {
            // Arrange
            var createItemViewModel = GetCreateItemViewModel(title: title);

            // Act
            var result = Validator.TryValidateObject(createItemViewModel,
                new ValidationContext(createItemViewModel), 
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
            var createItemViewModel = GetCreateItemViewModel(comment: comment);

            // Act
            var result = Validator.TryValidateObject(createItemViewModel,
                new ValidationContext(createItemViewModel),
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
            var createItemViewModel = GetCreateItemViewModel(quantity: quantity);

            // Act
            var result = Validator.TryValidateObject(createItemViewModel,
                new ValidationContext(createItemViewModel),
                null,
                true);

            // Assert
            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestItemListIdData), DynamicDataSourceType.Method)]
        public void TestItemQuantityValidation(string displayName,
            int itemListId)
        {
            // Arrange
            var createItemViewModel = GetCreateItemViewModel(itemListId: itemListId);

            // Act
            var result = Validator.TryValidateObject(createItemViewModel,
                new ValidationContext(createItemViewModel),
                null,
                true);

            // Assert
            Assert.IsTrue(result);
        }

        private static IEnumerable<object[]> GetTestTitleData()
        {
            yield return new object[]
            {
                "Test case 1: Create_CreateItemViewModel_Given_Valid_Title_equals_min_value_" +
                "Validation_should_be_successful",
                "*"
            };

            yield return new object[]
            {
                "Test case 2: Create_CreateItemViewModel_Given_Valid_Title_equals_max_value_" +
                "Validation_should_be_successful",
                new string('*', 100)
            };

            yield return new object[]
            {
                "Test case 3: Create_CreateItemViewModel_Given_Valid_Title_equals_average_value_" +
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

        private static IEnumerable<object[]> GetTestItemListIdData()
        {
            yield return new object[]
            {
                "Test case 1: Create_CreateItemViewModel_Given_Valid_ItemListId_equals_min_value_" +
                "Validation_should_be_successful",
                1
            };

            yield return new object[]
            {
                "Test case 2: Create_CreateItemViewModel_Given_Valid_ItemListId_equals_average_value_" +
                "Validation_should_be_successful",
                2500
            };

            yield return new object[]
            {
                "Test case 3: Create_CreateItemViewModel_Given_Valid_ItemListId_equals_max_value_" +
                "Validation_should_be_successful",
                int.MaxValue
            };
        }

        private static CreateItemViewModel GetCreateItemViewModel(string title = "Valid value", 
            string comment = null, string quantity = null, 
            int itemListId = 1)
        {
            return new CreateItemViewModel()
            {
                Title = title,
                Comment = comment,
                Quantity = quantity,
                ItemListId = itemListId
            };
        }
    }
}
