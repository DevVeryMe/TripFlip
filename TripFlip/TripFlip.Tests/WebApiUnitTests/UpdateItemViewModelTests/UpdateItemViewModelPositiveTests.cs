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
        [DynamicData(nameof(GetUpdateItemViewModelData), DynamicDataSourceType.Method)]
        public void TestItemViewModelValidation(string displayName,
            UpdateItemViewModel updateItemViewModel)
        {
            // Act
            var result = Validator.TryValidateObject(updateItemViewModel,
                new ValidationContext(updateItemViewModel),
                null,
                true);

            // Assert
            Assert.IsTrue(result);
        }

        private static IEnumerable<object[]> GetUpdateItemViewModelData()
        {
            yield return new object[]
            {
                "Test case 1: Create_UpdateItemViewModel_Given_Valid_null_values_where_possible_" +
                "Validation_should_be_successful",
                GetUpdateItemViewModel(id: 1, title: "Tent", comment: null, 
                    quantity: null, isCompleted: true)
            };

            yield return new object[]
            {
                "Test case 2: Create_UpdateItemViewModel_Given_Valid_without_null_values_where_" +
                "possible_Validation_should_be_successful",
                GetUpdateItemViewModel(id: 1, title: "Tent", comment: "The most needed item.", 
                    quantity: "At least 1.", isCompleted: true)
            };

            yield return new object[]
            {
                "Test case 3: Create_UpdateItemViewModel_Given_Valid_Comment_equals_null_" +
                "Validation_should_be_successful",
                GetUpdateItemViewModel(id: 1, title: "Tent", comment: null, 
                    quantity: "At least 1.", isCompleted: true)
            };

            yield return new object[]
            {
                "Test case 4: Create_UpdateItemViewModel_Given_Valid_Quantity_equals_null_" +
                "Validation_should_be_successful",
                GetUpdateItemViewModel(id: 1, title: "Tent", comment: "The most needed item.", 
                    quantity: null, isCompleted: true)
            };
        }

        private static UpdateItemViewModel GetUpdateItemViewModel(int id = 1, string title = "Tent", 
            string comment = null, string quantity = null, bool isCompleted = false)
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
