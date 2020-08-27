using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.ItemViewModels;

namespace WebApiUnitTests.ItemViewModels
{
    [TestClass]
    public class UpdateItemCompletenessViewModelPositiveTests
    {
        [DataTestMethod]
        public void Create_UpdateItemCompletenessViewModel_Given_Valid_values_Should_be_successful()
        {
            var updateItemCompletenessViewModel = GetUpdateItemCompletenessViewModel(1, true);

            var result = Validator.TryValidateObject(updateItemCompletenessViewModel,
                new ValidationContext(updateItemCompletenessViewModel),
                null,
                true);

            Assert.IsTrue(result);
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
