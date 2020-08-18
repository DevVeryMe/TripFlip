using System;

namespace TripFlip.ViewModels.RouteViewModels
{
    /// <summary>
    /// ViewModel that represents the result of a successfully completed action. This ViewModel is meant to be returned back to user as an action result
    /// </summary>
    public class ResultRouteViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public int TripId { get; set; }
    }
}
