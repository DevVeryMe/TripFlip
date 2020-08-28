using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.ItemListViewModels;

namespace WebApiUnitTests.ItemList
{
    [TestClass]
    public class CreateItemListViewModel_Negative_Tests
    {
        [DataTestMethod]
        [DynamicData(nameof(Get_Invalid_ItemList_Title), DynamicDataSourceType.Method)]
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

        [TestMethod]
        public void Test_ItemList_RouteId_Validation()
        {
            // Arrange
            string validTitle = "Valid item list title.";
            int invalidRouteId = -1;
            var createItemListViewModel = new CreateItemListViewModel()
            {
                Title = validTitle,
                RouteId = invalidRouteId
            };

            // Act
            bool result = Validator.TryValidateObject(createItemListViewModel,
                new ValidationContext(createItemListViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsFalse(result);
        }

        static IEnumerable<object[]> Get_Invalid_ItemList_Title()
        {
            yield return new object[]
            {
                "Test case 1 : Test_ItemList_Title_Validation was given invalid title equals null." +
                " Validation should be failed.",
                null,
                1
            };

            yield return new object[]
            {
                "Test case 2 : Test_ItemList_Title_Validation was given invalid empty Title string." +
                " Validation should be failed.",
                string.Empty,
                1
            };

            yield return new object[]
            {
                "Test case 3 : Test_ItemList_Title_Validation was given invalid title string" +
                " with the length of 101 (which exceeds allowed string length of 100 characters)." +
                " Validation should be failed.",
                new string('*', 101),
                1
            };
        }
    }
}
