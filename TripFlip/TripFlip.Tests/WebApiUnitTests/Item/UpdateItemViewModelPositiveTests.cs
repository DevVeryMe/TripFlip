using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.ItemViewModels;

namespace WebApiUnitTests.Item
{
    [TestClass]
    public class UpdateItemViewModelPositiveTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetUpdateItemViewModelData), DynamicDataSourceType.Method)]
        public void TestItemViewModelValidation(string displayName,
            UpdateItemViewModel updateItemViewModel)
        {
            var result = Validator.TryValidateObject(updateItemViewModel,
                new ValidationContext(updateItemViewModel),
                null,
                true);

            Assert.IsTrue(result);
        }

        private static IEnumerable<object[]> GetUpdateItemViewModelData()
        {
            yield return new object[]
            {
                "Test case 1: Create_CreateItemViewModel_Given_Valid_null_values_where_possible_" +
                "Validation_should_be_successful",
                GetUpdateItemViewModel(1, "Tent", null, null, true)
            };

            yield return new object[]
            {
                "Test case 2: Create_CreateItemViewModel_Given_Valid_without_null_values_where_" +
                "possible_Validation_should_be_successful",
                GetUpdateItemViewModel(1, "Tent", "The most needed item.", "At least 1.", true)
            };

            yield return new object[]
            {
                "Test case 3: Create_CreateItemViewModel_Given_Valid_Comment_equals_null_" +
                "Validation_should_be_successful",
                GetUpdateItemViewModel(1, "Tent", null, "At least 1.", true)
            };

            yield return new object[]
            {
                "Test case 4: Create_CreateItemViewModel_Given_Valid_Quantity_equals_null_" +
                "Validation_should_be_successful",
                GetUpdateItemViewModel(1, "Tent", "The most needed item.", null, true)
            };
        }

        private static UpdateItemViewModel GetUpdateItemViewModel(int id, string title, 
            string comment, string quantity, bool isCompleted)
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
