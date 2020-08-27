using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.ItemViewModels;

namespace WebApiUnitTests.ItemViewModels
{
    [TestClass]
    public class CreateItemViewModelPositiveTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetCreateItemViewModelData), DynamicDataSourceType.Method)]
        public void TestItemViewModelValidation(string displayName,
            CreateItemViewModel createItemViewModel)
        {
            var result = Validator.TryValidateObject(createItemViewModel,
                new ValidationContext(createItemViewModel), 
                null, 
                true);

            Assert.IsTrue(result);
        }

        private static IEnumerable<object[]> GetCreateItemViewModelData()
        {
            yield return new object[]
            {
                "Test case 1: Create_CreateItemViewModel_Given_Valid_Not_null_values_Should_be_successful",
                GetCreateItemViewModel("Tent", null, null, 1)
            };

            yield return new object[]
            {
                "Test case 1: Create_CreateItemViewModel_Given_Valid_With_null_values_where_possible_Should_be_successful",
                GetCreateItemViewModel("Tent", "The most needed item.", "At least 1.", 1)
            };
        }

        private static CreateItemViewModel GetCreateItemViewModel(string title, string comment,
            string quantity, int itemListId)
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
