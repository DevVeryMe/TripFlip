using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.ItemViewModels;

namespace WebApiUnitTests.Item
{
    [TestClass]
    public class UpdateItemCompletenessViewModelNegativeTests
    {
        [DataTestMethod]
        public void Create_UpdateItemCompletenessViewModel_Given_Not_valid_Id_Validation_should_be_failed()
        {
            var updateItemCompletenessViewModel = GetUpdateItemCompletenessViewModel(-1, true);

            var result = Validator.TryValidateObject(updateItemCompletenessViewModel,
                new ValidationContext(updateItemCompletenessViewModel),
                null,
                true);

            Assert.IsFalse(result);
        }

        private static UpdateItemCompletenessViewModel GetUpdateItemCompletenessViewModel(int id, bool isCompleted)
        {
            return new UpdateItemCompletenessViewModel()
            {
                Id = id,
                IsCompleted = isCompleted
            };
        }
    }
}
