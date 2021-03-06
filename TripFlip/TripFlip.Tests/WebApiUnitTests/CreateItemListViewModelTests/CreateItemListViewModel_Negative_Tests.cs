﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.ItemListViewModels;

namespace WebApiUnitTests.CreateItemListViewModelTests
{
    [TestClass]
    public class CreateItemListViewModel_Negative_Tests
    {
        [DataTestMethod]
        [DynamicData(nameof(Get_Invalid_ItemList_Title), DynamicDataSourceType.Method)]
        public void Test_ItemList_Title_Validation(
            string testCaseDisplayName,
            string invalidTitle)
        {
            // Arrange
            CreateItemListViewModel createItemListViewModel = 
                Get_CreateItemListViewModel(title: invalidTitle);

            // Act
            bool result = Validator.TryValidateObject(createItemListViewModel,
                new ValidationContext(createItemListViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsFalse(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(Get_Invalid_ItemList_RouteId), DynamicDataSourceType.Method)]
        public void Test_ItemList_RouteId_Validation(
            string testCaseDisplayName,
            int invalidRouteId)
        {
            // Arrange
            CreateItemListViewModel createItemListViewModel = 
                Get_CreateItemListViewModel(routeId: invalidRouteId);

            // Act
            bool result = Validator.TryValidateObject(createItemListViewModel,
                new ValidationContext(createItemListViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsFalse(result, testCaseDisplayName);
        }

        static IEnumerable<object[]> Get_Invalid_ItemList_Title()
        {
            yield return new object[]
            {
                "Test case 1 : Test_ItemList_Title_Validation was given invalid title equals null." +
                " Validation should be failed.",
                null
            };

            yield return new object[]
            {
                "Test case 2 : Test_ItemList_Title_Validation was given invalid empty Title string." +
                " Validation should be failed.",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 3 : Test_ItemList_Title_Validation was given invalid title string" +
                " with the length of 101 (which exceeds allowed string length of 100 characters)." +
                " Validation should be failed.",
                new string('*', 101)
            };
        }

        static IEnumerable<object[]> Get_Invalid_ItemList_RouteId()
        {
            yield return new object[]
            {
                "Test case 1 : Test_ItemList_RouteId_Validation was given invalid" +
                " routeId value that is negative integer number (-1)." +
                " Validation should be failed.",
                -1
            };

            yield return new object[]
            {
                "Test case 2 : Test_ItemList_RouteId_Validation was given invalid" +
                " routeId value that equals 0." +
                " Validation should be failed.",
                0
            };
        }

        CreateItemListViewModel Get_CreateItemListViewModel(
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
