using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TripFlip.ViewModels;
using TripFlip.ViewModels.RouteViewModels;

namespace WebApiUnitTests.RouteViewModels.Negative
{
    [TestClass]
    public class CreateRouteViewModelNegativeTests
    {
        #region TestMethods

        [DataTestMethod]
        [DynamicData(nameof(GetInvalidTitleData), DynamicDataSourceType.Method)]
        public void Title_IsEmpty_ExceptionThrown(string displayName, string notValidTitle)
        {
            // Arrange.
            int validTripId = 1;

            var createRouteViewModel = BuildCreateRouteViewModel(notValidTitle, validTripId);

            // Act.
            var titleValidationError = ValidateModel(createRouteViewModel);

            var result = (titleValidationError
                .Where(error => error.ErrorMessage.Contains(ErrorConstants.EmptyTitleFieldError)).Count() > 0);

            // Assert.
            Assert.IsTrue(result, displayName);
        }

        [TestMethod]
        public void Title_LengthMoreThanAllowed_ExceptionThrown()
        {
            // Arrange.
            string notValidTitle = new string('*', 101);
            int validTripId = 1;

            var createRouteViewModel = BuildCreateRouteViewModel(notValidTitle, validTripId);

            // Act.
            var titleError = ValidateModel(createRouteViewModel);

            var result = (titleError
                .Where(error => error.ErrorMessage.Contains(ErrorConstants.TitleLengthError)).Count() > 0);

            // Assert.
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TripId_IsNegativeNumber_ExceptionThrown()
        {
            // Arrange.
            string validTitle = new string('*', 3);
            int notValidTripId = -1;

            var createRouteViewModel = BuildCreateRouteViewModel(validTitle, notValidTripId);

            // Act.
            var titleError = ValidateModel(createRouteViewModel);

            var result = (titleError
                .Where(error => error.ErrorMessage.Contains(ErrorConstants.IdLessThanOneError)).Count() > 0);

            // Assert.
            Assert.IsTrue(result);
        }

        #endregion

        #region HelpingMethods

        private static CreateRouteViewModel BuildCreateRouteViewModel(string title, int tripId)
        {
            CreateRouteViewModel createRouteViewModel = new CreateRouteViewModel()
            {
                Title = title,
                TripId = tripId
            };

            return createRouteViewModel;
        }

        private static IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);

            Validator.TryValidateObject(model, validationContext, validationResults, true);

            return validationResults;
        }

        private static IEnumerable<object[]> GetInvalidTitleData()
        {
            yield return new object[]
            {
                "Test case 1: Title_IsNull_ExceptionThrown",
                null
            };

            yield return new object[]
            {
                "Test case 2: Title_IsEmptyString_ExceptionThrown",
                string.Empty
            };
        }

        #endregion
    }
}
