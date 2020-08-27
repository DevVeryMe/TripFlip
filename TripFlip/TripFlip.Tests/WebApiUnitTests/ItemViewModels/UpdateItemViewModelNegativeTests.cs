using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.ItemViewModels;

namespace WebApiUnitTests.ItemViewModels
{
    [TestClass]
    public class UpdateItemViewModelNegativeTests
    {
        [DataTestMethod]
        public void Create_UpdateItemViewModel_Given_Not_valid_Id_Validation_should_be_failed()
        {
            var updateItemViewModel = GetUpdateItemViewModel(0, "Tent", null, null, true);

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
            var updateItemViewModel = GetUpdateItemViewModel(1, title, null, null, true);

            var result = Validator.TryValidateObject(updateItemViewModel,
                new ValidationContext(updateItemViewModel),
                null,
                true);

            Assert.IsFalse(result);
        }

        [DataTestMethod]
        public void Create_UpdateItemViewModel_Given_Not_valid_Comment_too_long_length_Validation_should_be_failed()
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

            var createItemViewModel = GetUpdateItemViewModel(1, "Tent", comment, null, true);

            var result = Validator.TryValidateObject(createItemViewModel,
                new ValidationContext(createItemViewModel),
                null,
                true);

            Assert.IsFalse(result);
        }

        [DataTestMethod]
        public void Create_UpdateItemViewModel_Given_Not_valid_Quantity_too_long_length_Validation_should_be_failed()
        {
            var quantity = "The European languages are members of the same family. Their separate " +
                          "existence is a myth.";

            var createItemViewModel = GetUpdateItemViewModel(1, "Tent", null, quantity, true);

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
                "Test case 1: Create_UpdateItemViewModel_Given_Not_valid_Title_equals_null_Validation_should_be_failed",
                null
            };

            yield return new object[]
            {
                "Test case 2: Create_UpdateItemViewModel_Given_Not_valid_Title_equals_empty_string_Validation_should_be_failed",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3: Create_UpdateItemViewModel_Given_Not_valid_Title_too_long_length_Validation_should_be_failed",

                "The European languages are members of the same family. Their separate existence is a myth. " +
                "For science, music, sport, etc, Europe uses the same vocabulary. The languages only differ in" +
                " their grammar, their pronunciation and their most common words."
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
