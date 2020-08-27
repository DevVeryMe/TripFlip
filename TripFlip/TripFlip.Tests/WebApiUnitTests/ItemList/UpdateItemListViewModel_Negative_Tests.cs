﻿using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.ItemListViewModels;

namespace WebApiUnitTests.ItemList
{
    [TestClass]
    public class UpdateItemListViewModel_Negative_Tests
    {
        [DataTestMethod]
        [DynamicData(nameof(Helpers.Get_Invalid_ItemList_Title), typeof(Helpers), DynamicDataSourceType.Method)]
        public void Test_ItemList_Title_Validation(
            string testCaseDisplayName,
            string title,
            int validId)
        {
            // Arrange
            var updateItemListViewModel = new UpdateItemListViewModel()
            {
                Id = validId,
                Title = title
            };

            // Act
            bool result = Validator.TryValidateObject(updateItemListViewModel,
                new ValidationContext(updateItemListViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsFalse(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(Helpers.Get_Invalid_ItemList_Id), typeof(Helpers), DynamicDataSourceType.Method)]
        public void Test_ItemList_Id_Validation(
            string testCaseDisplayName,
            string validTitle,
            int id)
        {
            // Arrange
            var updateItemListViewModel = new UpdateItemListViewModel()
            {
                Id = id,
                Title = validTitle
            };

            // Act
            bool result = Validator.TryValidateObject(updateItemListViewModel,
                new ValidationContext(updateItemListViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsFalse(result, testCaseDisplayName);
        }
    }
}
