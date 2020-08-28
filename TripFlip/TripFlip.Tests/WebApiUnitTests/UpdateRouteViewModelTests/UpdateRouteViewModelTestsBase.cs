using TripFlip.ViewModels.RouteViewModels;

namespace WebApiUnitTests.UpdateRouteViewModelTests
{
    public abstract class UpdateRouteViewModelTestsBase
    {
        /// <summary>
        /// Creates UpdateRouteViewModel object with given Id, Title and Trip id.
        /// </summary>
        /// <param name="id">Route id.</param>
        /// <param name="title">Route title.</param>
        /// <param name="tripId">Trip id.</param>
        /// <returns>Created UpdateRouteViewModel object.</returns>
        protected static UpdateRouteViewModel BuildUpdateRouteViewModel(int id, string title, int tripId)
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
