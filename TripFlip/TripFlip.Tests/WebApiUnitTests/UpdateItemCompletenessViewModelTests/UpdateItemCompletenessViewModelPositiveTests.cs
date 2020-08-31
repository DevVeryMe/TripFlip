using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.ItemViewModels;

namespace WebApiUnitTests.UpdateItemCompletenessViewModelTests
{
    [TestClass]
    public class UpdateItemCompletenessViewModelPositiveTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestIdData), DynamicDataSourceType.Method)]
        public void TestIdValidation(string displayName, int id)
        {
            // Arrange
            var updateItemCompletenessViewModel = GetUpdateItemCompletenessViewModel(id: id);

            // Act
            var result = Validator.TryValidateObject(updateItemCompletenessViewModel,
                new ValidationContext(updateItemCompletenessViewModel),
                null,
                true);

            // Assert
            Assert.IsTrue(result);
        }

        private static IEnumerable<object[]> GetTestIdData()
        {
            yield return new object[]
            {
                "Test case 1: Create_UpdateItemCompletenessViewModel_Given_Valid_Id_min_value_" +
                "Validation_should_be_successful",
                1
            };

            yield return new object[]
            {
                "Test case 2: Create_UpdateItemCompletenessViewModel_Given_Valid_Id_max_value_" +
                "Validation_should_be_successful",
                int.MaxValue
            };

            yield return new object[]
            {
                "Test case 3: Create_UpdateItemCompletenessViewModel_Given_Valid_Id_average_value_" +
                "Validation_should_be_successful",
                2500
            };
        }

        private static UpdateItemCompletenessViewModel GetUpdateItemCompletenessViewModel(
            int id = 1, bool isCompleted = false)
        {
            return new UpdateItemCompletenessViewModel()
            {
                Id = id,
                IsCompleted = isCompleted
            };
        }
    }
}
