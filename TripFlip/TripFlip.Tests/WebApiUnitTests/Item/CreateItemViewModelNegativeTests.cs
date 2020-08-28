using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.ItemViewModels;

namespace WebApiUnitTests.Item
{
    [TestClass]
    public class CreateItemViewModelNegativeTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestItemTitleData), DynamicDataSourceType.Method)]
        public void TestItemTitleValidation(string displayName, string title)
        {
            var createItemViewModel = GetCreateItemViewModel(title: title);

            var result = Validator.TryValidateObject(createItemViewModel,
                new ValidationContext(createItemViewModel),
                null,
                true);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Create_CreateItemViewModel_Given_Not_valid_Comment_too_long_Validation_should_be_failed()
        {
            var comment = new string('*', 251);
            var createItemViewModel = GetCreateItemViewModel(comment: comment);

            var result = Validator.TryValidateObject(createItemViewModel,
                new ValidationContext(createItemViewModel),
                null,
                true);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Create_CreateItemViewModel_Given_Not_valid_Quantity_too_long_Validation_should_be_failed()
        {
            var quantity = new string('*', 51);

            var createItemViewModel = GetCreateItemViewModel(quantity: quantity);

            var result = Validator.TryValidateObject(createItemViewModel,
                new ValidationContext(createItemViewModel),
                null,
                true);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Create_CreateItemViewModel_Given_Not_valid_ItemListId_Validation_should_be_failed()
        {
            var createItemViewModel = GetCreateItemViewModel(itemListId: -1);

            var result = Validator.TryValidateObject(createItemViewModel,
                new ValidationContext(createItemViewModel),
                null,
                true);

            Assert.IsFalse(result);
        }

        private static IEnumerable<object[]> GetTestItemTitleData()
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

        private static CreateItemViewModel GetCreateItemViewModel(string title = "Tent",
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
