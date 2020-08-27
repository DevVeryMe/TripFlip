using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.ItemListViewModels;

namespace WebApiUnitTests.ItemList
{
    [TestClass]
    public class CreateItemListViewModel_Positive_Tests
    {
        [TestMethod]
        public void Test_ItemList_Validation()
        {
            // Arrange
            string validTitle = "Item list title.";
            int validRouteId = 1;
            var createItemListViewModel = new CreateItemListViewModel()
            {
                Title = validTitle,
                RouteId = validRouteId
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
