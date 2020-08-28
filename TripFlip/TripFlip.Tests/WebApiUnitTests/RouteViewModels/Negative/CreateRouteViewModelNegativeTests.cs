using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TripFlip.ViewModels;
using WebApiUnitTests.RouteViewModels.Helpers;

namespace WebApiUnitTests.RouteViewModels.Negative
{
    [TestClass]
    public class CreateRouteViewModelNegativeTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetInvalidTitleData), DynamicDataSourceType.Method)]
        public void Title_IsEmpty_ExceptionThrown(string displayName, string notValidTitle)
        {
            // Arrange.
            int validTripId = 1;

            var createRouteViewModel = RouteViewModelsBuilder
                .BuildCreateRouteViewModel(notValidTitle, validTripId);

            // Act.
            var validationResults = ModelValidator.Validate(createRouteViewModel);

            bool modelIsNotValid = (validationResults
                .Where(error => error.ErrorMessage.Contains(ErrorConstants.EmptyTitleFieldError)).Count() > 0);

            // Assert.
            Assert.IsTrue(modelIsNotValid, displayName);
        }

        [TestMethod]
        public void Title_LengthMoreThanAllowed_ExceptionThrown()
        {
            // Arrange.
            string notValidTitle = new string('*', 101);
            int validTripId = 1;

            var createRouteViewModel = RouteViewModelsBuilder
                .BuildCreateRouteViewModel(notValidTitle, validTripId);

            // Act.
            var validationResults = ModelValidator.Validate(createRouteViewModel);

            bool modelIsNotValid = (validationResults
                .Where(error => error.ErrorMessage.Contains(ErrorConstants.TitleLengthError)).Count() > 0);

            // Assert.
            Assert.IsTrue(modelIsNotValid);
        }

        [TestMethod]
        public void TripId_IsNegativeNumber_ExceptionThrown()
        {
            // Arrange.
            string validTitle = new string('*', 3);
            int notValidTripId = -1;

            var createRouteViewModel = RouteViewModelsBuilder
                .BuildCreateRouteViewModel(validTitle, notValidTripId);

            // Act.
            var validationResults = ModelValidator.Validate(createRouteViewModel);

            bool modelIsNotValid = (validationResults
                .Where(error => error.ErrorMessage.Contains(ErrorConstants.IdLessThanOneError)).Count() > 0);

            // Assert.
            Assert.IsTrue(modelIsNotValid);
        }

        private static IEnumerable<object[]> GetInvalidTitleData()
        {
            yield return new object[]
            {
                "Test case 1: Test CreateRouteViewModelBuilder validation" +
                " if title set to null. Validation should fail.",
                null
            };

            yield return new object[]
            {
                "Test case 2: Test CreateRouteViewModelBuilder validation" +
                " if title set to empty string. Validation should fail.",
                string.Empty
            };
        }
    }
}
