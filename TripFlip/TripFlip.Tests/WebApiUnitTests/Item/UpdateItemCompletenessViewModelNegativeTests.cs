using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.ItemViewModels;

namespace WebApiUnitTests.Item
{
    [TestClass]
    public class UpdateItemCompletenessViewModelNegativeTests
    {
        [TestMethod]
        public void Create_UpdateItemCompletenessViewModel_Given_Not_valid_Id_Validation_should_be_failed()
        {
            var updateItemCompletenessViewModel = GetUpdateItemCompletenessViewModel(id: -1);

            var result = Validator.TryValidateObject(updateItemCompletenessViewModel,
                new ValidationContext(updateItemCompletenessViewModel),
                null,
                true);

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
