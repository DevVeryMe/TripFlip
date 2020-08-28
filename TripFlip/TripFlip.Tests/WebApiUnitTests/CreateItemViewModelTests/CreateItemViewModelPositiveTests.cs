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
        [DynamicData(nameof(GetCreateItemViewModelData), DynamicDataSourceType.Method)]
        public void TestItemViewModelValidation(string displayName,
            CreateItemViewModel createItemViewModel)
        {
            // Act
            var result = Validator.TryValidateObject(createItemViewModel,
                new ValidationContext(createItemViewModel), 
                null, 
                true);

            // Assert
            Assert.IsTrue(result);
        }

        private static IEnumerable<object[]> GetCreateItemViewModelData()
        {
            yield return new object[]
            {
                "Test case 1: Create_CreateItemViewModel_Given_Valid_null_values_where_possible_" +
                "Validation_should_be_successful",
                GetCreateItemViewModel(title: "Tent", comment: null, quantity: null, itemListId: 1)
            };

            yield return new object[]
            {
                "Test case 2: Create_CreateItemViewModel_Given_Valid_without_null_values_where_" +
                "possible_Validation_should_be_successful",
                GetCreateItemViewModel(title: "Tent", comment: "The most needed item.", 
                    quantity: "At least 1.", itemListId: 1)
            };

            yield return new object[]
            {
                "Test case 3: Create_CreateItemViewModel_Given_Valid_Comment_equals_null_" +
                "Validation_should_be_successful",
                GetCreateItemViewModel(title: "Tent", comment: null, 
                    quantity: "At least 1.", itemListId: 1)
            };

            yield return new object[]
            {
                "Test case 4: Create_CreateItemViewModel_Given_Valid_Quantity_equals_null_" +
                "Validation_should_be_successful",
                GetCreateItemViewModel(title: "Tent", comment: "The most needed item.", 
                    quantity: null, itemListId: 1)
            };
        }

        private static CreateItemViewModel GetCreateItemViewModel(string title = "Tent", 
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
