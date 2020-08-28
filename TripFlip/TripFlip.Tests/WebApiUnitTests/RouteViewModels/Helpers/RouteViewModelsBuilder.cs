using TripFlip.ViewModels.RouteViewModels;

namespace WebApiUnitTests.RouteViewModels.Helpers
{
    static class RouteViewModelsBuilder
    {
        public static CreateRouteViewModel BuildCreateRouteViewModel(string title, int tripId)
        {
            var createRouteViewModel = new CreateRouteViewModel()
            {
                Title = title,
                TripId = tripId
            };

            return createRouteViewModel;
        }

        public static UpdateRouteViewModel BuildUpdateRouteViewModel(int id, string title, int tripId)
        {
            var updateRouteViewModel = new UpdateRouteViewModel()
            {
                Id  = id,
                Title = title,
                TripId = tripId
            };

            return updateRouteViewModel;
        }
    }
}
