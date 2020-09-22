using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.TaskViewModels;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.TaskAssigneesViewModelTests
{
    [TestClass]
    public class TaskAssigneesViewModel_Positive_Tests
    {
        static readonly IEnumerable<int> _defaultRouteSubscriberIds = new int[] { 1, 2, 3 };

        [DataTestMethod]
        [DynamicData(nameof(Get_Valid_TaskId), DynamicDataSourceType.Method)]
        public void Test_TaskAssignees_TaskId_Validation(
            string testCaseDisplayName,
            int validTaskId)
        {
            // Arrange
            TaskAssigneesViewModel taskAssigneesViewModel =
                Get_Valid_TaskAssigneesViewModel(
                    taskId: validTaskId,
                    routeSubscriberIds: _defaultRouteSubscriberIds);

            // Act
            bool result = ModelValidator.IsValid(taskAssigneesViewModel);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(Get_Valid_RouteSubscriberIds), DynamicDataSourceType.Method)]
        public void Test_TaskAssignees_RouteSubscriberIds_Validation(
            string testCaseDisplayName,
            IEnumerable<int> validRouteSubscriberIds)
        {
            // Arrange
            TaskAssigneesViewModel taskAssigneesViewModel =
                Get_Valid_TaskAssigneesViewModel(routeSubscriberIds: validRouteSubscriberIds);

            // Act
            bool result = ModelValidator.IsValid(taskAssigneesViewModel);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        static IEnumerable<object[]> Get_Valid_TaskId()
        {
            yield return new object[]
            {
                "Test case 1 : Test_TaskAssignees_TaskId_Validation was given Id with minimal" +
                " valid value that equals 1. Validation should be successful.",
                1
            };

            yield return new object[]
            {
                "Test case 2 : Test_TaskAssignees_TaskId_Validation was given Id with maximum" +
                " valid length that equals maximum number that is supported by integer." +
                " Validation should be successful.",
                int.MaxValue
            };
        }

        static IEnumerable<object[]> Get_Valid_RouteSubscriberIds()
        {
            yield return new object[]
            {
                "Test case 1 : Test_TaskAssignees_RouteSubscriberIds_Validation " +
                "was given valid route subscriber ids collection that is empty." +
                " Validation should be successful.",
                new int[] { }
            };

            yield return new object[]
            {
                "Test case 2 : Test_TaskAssignees_RouteSubscriberIds_Validation " +
                "was given valid route subscriber ids collection with different " +
                "values. " +
                " Validation should be successful.",
                new int[] {1, 2, int.MaxValue}
            };
        }

        TaskAssigneesViewModel Get_Valid_TaskAssigneesViewModel(
            int taskId = 1,
            IEnumerable<int> routeSubscriberIds = null)
        {
            return new TaskAssigneesViewModel()
            {
                TaskId = taskId,
                RouteSubscriberIds = routeSubscriberIds
            };
        }
    }
}
