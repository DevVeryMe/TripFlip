﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.ItemViewModels;

namespace WebApiUnitTests.CreateItemViewModelTests
{
    [TestClass]
    public class CreateItemViewModelNegativeTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestTitleData), DynamicDataSourceType.Method)]
        public void TestItemTitleValidation(string displayName, string title)
        {
            // Arrange
            var createItemViewModel = GetCreateItemViewModel(title: title);

            // Act
            var result = Validator.TryValidateObject(createItemViewModel,
                new ValidationContext(createItemViewModel),
                null,
                true);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Create_CreateItemViewModel_Given_Not_valid_Comment_too_long_Validation_should_be_failed()
        {
            // Arrange
            var comment = new string('*', 251);
            var createItemViewModel = GetCreateItemViewModel(comment: comment);

            // Act
            var result = Validator.TryValidateObject(createItemViewModel,
                new ValidationContext(createItemViewModel),
                null,
                true);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Create_CreateItemViewModel_Given_Not_valid_Quantity_too_long_Validation_should_be_failed()
        {
            // Arrange
            var quantity = new string('*', 51);
            var createItemViewModel = GetCreateItemViewModel(quantity: quantity);

            // Act
            var result = Validator.TryValidateObject(createItemViewModel,
                new ValidationContext(createItemViewModel),
                null,
                true);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Create_CreateItemViewModel_Given_Not_valid_ItemListId_Validation_should_be_failed()
        {
            // Arrange
            var createItemViewModel = GetCreateItemViewModel(itemListId: -1);

            // Act
            var result = Validator.TryValidateObject(createItemViewModel,
                new ValidationContext(createItemViewModel),
                null,
                true);

            // Assert
            Assert.IsFalse(result);
        }

        private static IEnumerable<object[]> GetTestTitleData()
        {
            yield return new object[]
            {
                "Test case 1: Create_CreateItemViewModel_Given_Not_valid_Title_equals_null_" +
                "Validation_should_be_failed",
                null
            };

            yield return new object[]
            {
                "Test case 2: Create_CreateItemViewModel_Given_Not_valid_Title_equals_empty_string_" +
                "Validation_should_be_failed",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3: Create_CreateItemViewModel_Given_Not_valid_Title_too_long_length_" +
                "Validation_should_be_failed",
                new string('*', 101)
            };
        }

        private static CreateItemViewModel GetCreateItemViewModel(string title = "Valid value",
            string comment = null, string quantity = null, int itemListId = 1)
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
