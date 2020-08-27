using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.ItemListViewModels;

namespace WebApiUnitTests.ItemList
{
    [TestClass]
    public class CreateItemListViewModel_Negative_Tests
    {
        [DataTestMethod]
        [DynamicData(nameof(Helpers.Get_Invalid_ItemList_Title), typeof(Helpers), DynamicDataSourceType.Method)]
        public void Test_ItemList_Title_Validation(
            string testCaseDisplayName,
            string title,
            int validRouteId)
        {
            // Arrange
            var createItemListViewModel = new CreateItemListViewModel()
            {
                Title = title,
                RouteId = validRouteId
            };

            // Act
            bool result = Validator.TryValidateObject(createItemListViewModel,
                new ValidationContext(createItemListViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsFalse(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(Helpers.Get_Invalid_ItemList_Id), typeof(Helpers), DynamicDataSourceType.Method)]
        public void Test_ItemList_RouteId_Validation(
           string testCaseDisplayName,
           string validTitle,
           int routeId)
        {
            // Arrange
            var createItemListViewModel = new CreateItemListViewModel()
            {
                Title = validTitle,
                RouteId = routeId
            };

            // Act
            bool result = Validator.TryValidateObject(createItemListViewModel,
                new ValidationContext(createItemListViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsFalse(result, testCaseDisplayName);
        }
    }
}
