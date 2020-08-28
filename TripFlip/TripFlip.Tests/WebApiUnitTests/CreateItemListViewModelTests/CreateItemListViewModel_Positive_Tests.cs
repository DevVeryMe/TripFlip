using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.ItemListViewModels;

namespace WebApiUnitTests.CreateItemListViewModelTests
{
    [TestClass]
    public class CreateItemListViewModel_Positive_Tests
    {
        [DataTestMethod]
        [DynamicData(nameof(Get_Valid_ItemList_Title), DynamicDataSourceType.Method)]
        public void Test_ItemList_Title_Validation(
            string testCaseDisplayName,
            string validTitle)
        {
            // Arrange
            CreateItemListViewModel createItemListViewModel =
                Get_Valid_CreateItemListViewModel(title: validTitle);

            // Act
            bool result = Validator.TryValidateObject(createItemListViewModel,
                new ValidationContext(createItemListViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(Get_Valid_ItemList_RouteId), DynamicDataSourceType.Method)]
        public void Test_ItemList_RouteId_Validation(
            string testCaseDisplayName,
            int validRouteId)
        {
            // Arrange
            CreateItemListViewModel createItemListViewModel =
                Get_Valid_CreateItemListViewModel(routeId: validRouteId);

            // Act
            bool result = Validator.TryValidateObject(createItemListViewModel,
                new ValidationContext(createItemListViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        static IEnumerable<object[]> Get_Valid_ItemList_Title()
        {
            yield return new object[]
            {
                "Test case 1 : Test_ItemList_Title_Validation was given title with minimal" +
                " valid length that equals 1. Validation should be successful.",
                new string('x', 1)
            };

            yield return new object[]
            {
                "Test case 2 : Test_ItemList_Title_Validation was given title with maximum" +
                " valid length that equals 100. Validation should be successful.",
                new string('x', 100)
            };
        }

        static IEnumerable<object[]> Get_Valid_ItemList_RouteId()
        {
            yield return new object[]
            {
                "Test case 1 : Test_ItemList_Title_Validation was given routeId with minimal" +
                " valid value that equals 1. Validation should be successful.",
                1
            };

            yield return new object[]
            {
                "Test case 2 : Test_ItemList_Title_Validation was given routeId with maximum" +
                " valid length that equals maximum number that is supported by integer." +
                " Validation should be successful.",
                int.MaxValue
            };
        }

        CreateItemListViewModel Get_Valid_CreateItemListViewModel(
            string title = "Valid item list title.",
            int routeId = 1)
        {
            return new CreateItemListViewModel()
            {
                Title = title,
                RouteId = routeId
            };
        }
    }
}
