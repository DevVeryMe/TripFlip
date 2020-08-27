using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.ItemViewModels;

namespace WebApiUnitTests.ItemViewModels
{
    [TestClass]
    public class CreateItemViewModelNegativeTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestItemTitleData), DynamicDataSourceType.Method)]
        public void TestItemTitleValidation(string displayName, string title)
        {
            var createItemViewModel = GetCreateItemViewModel(title, null, null, 1);

            var result = Validator.TryValidateObject(createItemViewModel,
                new ValidationContext(createItemViewModel),
                null,
                true);

            Assert.IsFalse(result);
        }

        [DataTestMethod]
        public void Create_CreateItemViewModel_Given_Not_valid_Comment_too_long_length_Validation_should_be_failed()
        {
            var comment = "The European languages are members of the same family. Their separate " +
                          "existence is a myth. For science, music, sport, etc, Europe uses the same" +
                          " vocabulary. The languages only differ in their grammar, their pronunciation" +
                          " and their most common words. Everyone realizes why a new common language would" +
                          " be desirable: one could refuse to pay expensive translators. To achieve this," +
                          " it would be necessary to have uniform grammar, pronunciation and more common words." +
                          " If several languages coalesce, the grammar of the resulting language is more" +
                          " simple and regular than that of the individual languages. The new common language" +
                          " will be more simple and regular than the existing European languages. It " +
                          "will be as simple as Occidental; in fact, it will be Occidental.";

                var createItemViewModel = GetCreateItemViewModel("Tent", comment, null, 1);

            var result = Validator.TryValidateObject(createItemViewModel,
                new ValidationContext(createItemViewModel),
                null,
                true);

            Assert.IsFalse(result);
        }

        [DataTestMethod]
        public void Create_CreateItemViewModel_Given_Not_valid_Quantity_too_long_length_Validation_should_be_failed()
        {
            var quantity = "The European languages are members of the same family. Their separate " +
                          "existence is a myth.";

            var createItemViewModel = GetCreateItemViewModel("Tent", null, quantity, 1);

            var result = Validator.TryValidateObject(createItemViewModel,
                new ValidationContext(createItemViewModel),
                null,
                true);

            Assert.IsFalse(result);
        }

        [DataTestMethod]
        public void Create_CreateItemViewModel_Given_Not_valid_ItemListId_Validation_should_be_failed()
        {
            var createItemViewModel = GetCreateItemViewModel("Tent", null, null, -1);

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
                "Test case 1: Create_CreateItemViewModel_Given_Not_valid_Title_equals_null_Validation_should_be_failed",
                null
            };

            yield return new object[]
            {
                "Test case 2: Create_CreateItemViewModel_Given_Not_valid_Title_equals_empty_string_Validation_should_be_failed",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3: Create_CreateItemViewModel_Given_Not_valid_Title_too_long_length_Validation_should_be_failed",

                "The European languages are members of the same family. Their separate existence is a myth. " +
                "For science, music, sport, etc, Europe uses the same vocabulary. The languages only differ in" +
                " their grammar, their pronunciation and their most common words."
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
