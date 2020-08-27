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
                "Test case 1: Create_UpdateItemViewModel_Given_Valid_Not_null_values_" +
                "Validation_should_be_successful",
                GetUpdateItemViewModel(1, "Tent", null, null,true)
            };

            yield return new object[]
            {
                "Test case 1: Create_UpdateItemViewModel_Given_Valid_With_null_values_where_" +
                "possible_Validation_should_be_successful",
                GetUpdateItemViewModel(1, "Tent", "The most needed item.", "At least 1.",true)
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
