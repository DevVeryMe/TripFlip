using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.ItemViewModels;

namespace WebApiUnitTests.UpdateItemCompletenessViewModelTests
{
    [TestClass]
    public class UpdateItemCompletenessViewModelNegativeTests
    {
        [TestMethod]
        public void Create_UpdateItemCompletenessViewModel_Given_Not_valid_Id_Validation_should_be_failed()
        {
            // Arrange
            var updateItemCompletenessViewModel = GetUpdateItemCompletenessViewModel(id: -1);

            // Act
            var result = Validator.TryValidateObject(updateItemCompletenessViewModel,
                new ValidationContext(updateItemCompletenessViewModel),
                null,
                true);

            // Assert
            Assert.IsFalse(result);
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
