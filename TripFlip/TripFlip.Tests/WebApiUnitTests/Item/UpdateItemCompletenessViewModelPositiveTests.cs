using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.ItemViewModels;

namespace WebApiUnitTests.Item
{
    [TestClass]
    public class UpdateItemCompletenessViewModelPositiveTests
    {
        [TestMethod]
        public void Create_UpdateItemCompletenessViewModel_Given_Valid_values_Validation_should_be_successful()
        {
            // Arrange
            var updateItemCompletenessViewModel = GetUpdateItemCompletenessViewModel(id: 1);

            // Act
            var result = Validator.TryValidateObject(updateItemCompletenessViewModel,
                new ValidationContext(updateItemCompletenessViewModel),
                null,
                true);

            // Assert
            Assert.IsTrue(result);
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
