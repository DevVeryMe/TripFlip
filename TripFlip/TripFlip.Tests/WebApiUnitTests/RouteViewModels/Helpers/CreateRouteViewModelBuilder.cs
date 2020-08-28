using TripFlip.ViewModels.RouteViewModels;

namespace WebApiUnitTests.RouteViewModels.Helpers
{
    static class CreateRouteViewModelBuilder
    {
        public static CreateRouteViewModel Build(string title, int tripId)
        {
            CreateRouteViewModel createRouteViewModel = new CreateRouteViewModel()
            {
                Title = title,
                TripId = tripId
            };

            return createRouteViewModel;
        }
    }
}
