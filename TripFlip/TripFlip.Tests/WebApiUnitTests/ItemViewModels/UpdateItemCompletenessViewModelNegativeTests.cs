using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.ItemViewModels;

namespace WebApiUnitTests.ItemViewModels
{
    [TestClass]
    public class UpdateItemCompletenessViewModelNegativeTests
    {
        [DataTestMethod]
        public void Create_UpdateItemCompletenessViewModel_Given_Not_valid_Id_Should_be_failed()
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
