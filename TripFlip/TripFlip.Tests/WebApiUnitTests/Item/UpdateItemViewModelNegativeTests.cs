using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.ItemViewModels;

namespace WebApiUnitTests.Item
{
    [TestClass]
    public class UpdateItemViewModelNegativeTests
    {
        [TestMethod]
        public void Create_UpdateItemViewModel_Given_Not_valid_Id_Validation_should_be_failed()
        {
            var updateItemViewModel = GetUpdateItemViewModel(id: 0);

            var result = Validator.TryValidateObject(updateItemViewModel,
                new ValidationContext(updateItemViewModel),
                null,
                true);

            Assert.IsFalse(result);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestItemTitleData), DynamicDataSourceType.Method)]
        public void TestItemTitleValidation(string displayName, string title)
        {
            var updateItemViewModel = GetUpdateItemViewModel(title: title);

            var result = Validator.TryValidateObject(updateItemViewModel,
                new ValidationContext(updateItemViewModel),
                null,
                true);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Create_UpdateItemViewModel_Given_Not_valid_Comment_too_long_Validation_should_be_failed()
        {
            var comment = new string('*', 251);

            var createItemViewModel = GetUpdateItemViewModel(comment: comment);

            var result = Validator.TryValidateObject(createItemViewModel,
                new ValidationContext(createItemViewModel),
                null,
                true);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Create_UpdateItemViewModel_Given_Not_valid_Quantity_too_long_Validation_should_be_failed()
        {
            var quantity = new string('*', 51);

            var createItemViewModel = GetUpdateItemViewModel(quantity: quantity);

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
                "Test case 1: Create_UpdateItemViewModel_Given_Not_valid_Title_equals_null_" +
                "Validation_should_be_failed",
                null
            };

            yield return new object[]
            {
                "Test case 2: Create_UpdateItemViewModel_Given_Not_valid_Title_equals_empty_string_" +
                "Validation_should_be_failed",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3: Create_UpdateItemViewModel_Given_Not_valid_Title_too_long_length_" +
                "Validation_should_be_failed",
                new string('*', 101)
            };
        }

        private static UpdateItemViewModel GetUpdateItemViewModel(int id = 1, string title = "Tent",
            string comment = "The most needed item.", string quantity = "At least one.", 
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
