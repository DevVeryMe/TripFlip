using System;

namespace TripFlip.ViewModels.RouteViewModels
{
    public class ResultRouteViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public int TripId { get; set; }
    }
}
