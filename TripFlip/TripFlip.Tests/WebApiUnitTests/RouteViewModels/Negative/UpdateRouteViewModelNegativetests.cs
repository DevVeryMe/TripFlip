using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TripFlip.ViewModels;
using WebApiUnitTests.RouteViewModels.Helpers;

namespace WebApiUnitTests.RouteViewModels.Negative
{
    [TestClass]
    public class UpdateRouteViewModelNegativeTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetInvalidTitleData), DynamicDataSourceType.Method)]
        public void Title_IsEmpty_ExceptionThrown(string displayName, string notValidTitle)
        {
            // Arrange.
            int validId = 1;
            int validTripId = 1;

            var updateRouteViewModel = RouteViewModelsBuilder
                .BuildUpdateRouteViewModel(validId, notValidTitle, validTripId);

            // Act.
            var titleValidationError = ModelValidator.Validate(updateRouteViewModel);

            bool modelIsNotValid = (titleValidationError
                .Where(error => error.ErrorMessage.Contains(ErrorConstants.EmptyTitleFieldError)).Count() > 0);

            // Assert.
            Assert.IsTrue(modelIsNotValid, displayName);
        }

        [TestMethod]
        public void Title_LengthMoreThanAllowed_ExceptionThrown()
        {
            // Arrange.
            int validId = 1;
            int validTripId = 1;
            string notValidTitle = new string('*', 101);

            var updateRouteViewModel = RouteViewModelsBuilder
                .BuildUpdateRouteViewModel(validId, notValidTitle, validTripId);

            // Act.
            var titleError = ModelValidator.Validate(updateRouteViewModel);

            bool modelIsNotValid = (titleError
                .Where(error => error.ErrorMessage.Contains(ErrorConstants.TitleLengthError)).Count() > 0);

            // Assert.
            Assert.IsTrue(modelIsNotValid);
        }

        [TestMethod]
        public void TripId_IsNegativeNumber_ExceptionThrown()
        {
            // Arrange.
            int validId = 1;
            int notValidTripId = -1;
            string validTitle = new string('*', 3);

            var updateRouteViewModel = RouteViewModelsBuilder
                .BuildUpdateRouteViewModel(validId, validTitle, notValidTripId);

            // Act.
            var validationResults = ModelValidator.Validate(updateRouteViewModel);

            bool modelIsNotValid = (validationResults
                .Where(error => error.ErrorMessage.Contains(ErrorConstants.IdLessThanOneError)).Count() > 0);

            // Assert.
            Assert.IsTrue(modelIsNotValid);
        }

        [TestMethod]
        public void Id_IsNegativeNumber_ExceptionThrown()
        {
            // Arrange.
            int notValidId = -1;
            int validTripId = 1;
            string validTitle = new string('*', 3);

            var updateRouteViewModel = RouteViewModelsBuilder
                .BuildUpdateRouteViewModel(notValidId, validTitle, validTripId);

            // Act.
            var validationResults = ModelValidator.Validate(updateRouteViewModel);

            bool modelIsNotValid = (validationResults
                .Where(error => error.ErrorMessage.Contains(ErrorConstants.IdLessThanOneError)).Count() > 0);

            // Assert.
            Assert.IsTrue(modelIsNotValid);
        }

        private static IEnumerable<object[]> GetInvalidTitleData()
        {
            yield return new object[]
            {
                "Test case 1: Test UpdateRouteViewModelBuilder validation" +
                " if title set to null. Validation should fail.",
                null
            };

            yield return new object[]
            {
                "Test case 1: Test UpdateRouteViewModelBuilder validation" +
                " if title set to empty string. Validation should fail.",
                string.Empty
            };
        }
    }
}
