using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.ItemAssigneeViewModelTests
{
    [TestClass]
    public class ItemAssigneeViewModelNegativeTests
        : ItemAssigneeViewModelTestsBase
    {
        [DataTestMethod]
        [DynamicData(nameof(GetInvalidItemIdData), DynamicDataSourceType.Method)]
        public void ItemId_IsNotValid_ExceptionThrown(string displayName,
            int notValidItemId)
        {
            // Arrange.
            var itemAssigneesViewModel =
                BuildItemAssigneesViewModel(
                    routeSubscriberIds: DefaultValidRouteSubscriberIds,
                    itemId: notValidItemId);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(itemAssigneesViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid, displayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetInvalidRouteSubscriberIdsData), DynamicDataSourceType.Method)]
        public void RouteSubscriberIds_AreNotValid_ExceptionThrown(string displayName,
            IEnumerable<int> notValidRoteSubscriberIds)
        {
            // Arrange.
            var itemAssigneesViewModel =
                BuildItemAssigneesViewModel(routeSubscriberIds: notValidRoteSubscriberIds);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(itemAssigneesViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid, displayName);
        }

        private static IEnumerable<object[]> GetInvalidItemIdData()
        {
            yield return new object[]
            {
                "Test case 1: Test ItemAssigneeViewModel validation" +
                " if ItemId is zero. Validation should fail.",
                0
            };

            yield return new object[]
            {
                "Test case 2: Test ItemAssigneeViewModel validation" +
                " if ItemId is negative number. Validation should fail.",
                -1
            };
        }

        private static IEnumerable<object[]> GetInvalidRouteSubscriberIdsData()
        {
            yield return new object[]
            {
                "Test case 1: Test ItemAssigneeViewModel validation" +
                " if RouteSubscriberIds array contains zero. Validation should fail.",
                new int[] { 0, 1, 2 }
            };

            yield return new object[]
            {
                "Test case 2: Test ItemAssigneeViewModel validation" +
                " if RouteSubscriberIds array contains negative number. Validation should fail.",
                new int[] { -1, 1, 2 }
            };

            yield return new object[]
            {
                "Test case 2: Test ItemAssigneeViewModel validation" +
                " if RouteSubscriberIds array contains repeated values. Validation should fail.",
                new int[] { 1, 1, 2 }
            };

            yield return new object[]
            {
                "Test case 2: Test ItemAssigneeViewModel validation" +
                " if RouteSubscriberIds array is null. Validation should fail.",
                null
            };
        }
    }
}
