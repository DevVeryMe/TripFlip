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
            var updateItemCompletenessViewModel = GetUpdateItemCompletenessViewModel(1, true);

            var result = Validator.TryValidateObject(updateItemCompletenessViewModel,
                new ValidationContext(updateItemCompletenessViewModel),
                null,
                true);

            Assert.IsTrue(result);
        }

        private static UpdateItemCompletenessViewModel GetUpdateItemCompletenessViewModel(
            int id, bool isCompleted)
        {
            return new UpdateItemCompletenessViewModel()
            {
                Id = id,
                IsCompleted = isCompleted
            };
        }
    }
}
