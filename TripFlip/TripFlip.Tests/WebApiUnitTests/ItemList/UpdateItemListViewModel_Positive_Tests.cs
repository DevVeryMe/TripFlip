using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.ItemListViewModels;

namespace WebApiUnitTests.ItemList
{
    [TestClass]
    public class UpdateItemListViewModel_Positive_Tests
    {
        [TestMethod]
        public void Test_ItemList_Validation()
        {
            // Arrange
            int validId = 1;
            string validTitle = "Item list title.";
            var createItemListViewModel = new UpdateItemListViewModel()
            {
                Id = validId,
                Title = validTitle
            };

            // Act
            bool result = Validator.TryValidateObject(createItemListViewModel,
                new ValidationContext(createItemListViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
