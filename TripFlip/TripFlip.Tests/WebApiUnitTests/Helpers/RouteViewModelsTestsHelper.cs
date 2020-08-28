using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.RouteViewModels;

namespace WebApiUnitTests.Helpers
{
    /// <summary>
    /// Helper class for Route view models test classes.
    /// </summary>
    static class RouteViewModelsTestsHelper
    {
        /// <summary>
        /// Validates any model object by its validation attributes.
        /// </summary>
        /// <param name="model">Model to validate.</param>
        /// <returns>True if model is valid and false if not.</returns>
        public static bool IsModelValid(object model)
        {
            var validationContext = new ValidationContext(model, null, null);

            bool isValid = Validator.TryValidateObject(model, validationContext, null, true);

            return isValid;
        }

        /// <summary>
        /// Creates CreateRouteViewModel object with given Title and Trip id.
        /// </summary>
        /// <param name="title">Route title.</param>
        /// <param name="tripId">Trip id.</param>
        /// <returns>Created CreateRouteViewModel object.</returns>
        public static CreateRouteViewModel BuildCreateRouteViewModel(string title, int tripId)
        {
            var createRouteViewModel = new CreateRouteViewModel()
            {
                Title = title,
                TripId = tripId
            };

            return createRouteViewModel;
        }

        /// <summary>
        /// Creates UpdateRouteViewModel object with given Id, Title and Trip id.
        /// </summary>
        /// <param name="id">Route id.</param>
        /// <param name="title">Route title.</param>
        /// <param name="tripId">Trip id.</param>
        /// <returns>Created UpdateRouteViewModel object.</returns>
        public static UpdateRouteViewModel BuildUpdateRouteViewModel(int id, string title, int tripId)
        {
            var updateRouteViewModel = new UpdateRouteViewModel()
            {
                Id = id,
                Title = title,
                TripId = tripId
            };

            return updateRouteViewModel;
        }
    }
}
