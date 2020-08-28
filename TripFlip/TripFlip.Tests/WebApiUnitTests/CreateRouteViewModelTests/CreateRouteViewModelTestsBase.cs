using TripFlip.ViewModels.RouteViewModels;

namespace WebApiUnitTests.CreateRouteViewModelTests
{
    public abstract class CreateRouteViewModelTestsBase
    {
        /// <summary>
        /// Creates CreateRouteViewModel object with given Title and Trip id.
        /// </summary>
        /// <param name="title">Route title.</param>
        /// <param name="tripId">Trip id.</param>
        /// <returns>Created CreateRouteViewModel object.</returns>
        protected static CreateRouteViewModel BuildCreateRouteViewModel(string title = "Default",
            int tripId = 3)
        {
            var createRouteViewModel = new CreateRouteViewModel()
            {
                Title = title,
                TripId = tripId
            };

            return createRouteViewModel;
        }
    }
}
